using Castle.DynamicProxy;
using CoreLayer.Utilities.Cache.Abstract;
using CoreLayer.Utilities.Cache.Concreate;
using CoreLayer.Utilities.Enum;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Messages;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace CoreLayer.Utilities.Aspect
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAspect : MethodInterceptionBase
    {
        private readonly ICacheWorkFlow _cacheWorkFlow;
        int _duration;
        TimeEnum _timeEnum;

        public override int Priority { get => int.MaxValue - 1; set => base.Priority = int.MaxValue - 1; }

        public CacheAspect(Type cacheWorkFlow, int duration = 30, TimeEnum timeEnum = TimeEnum.Minute)
        {
            if (!typeof(ICacheWorkFlow).IsAssignableFrom(cacheWorkFlow))
                throw new System.Exception("Hatalı cache belirtildi.");
            this._cacheWorkFlow = (ICacheWorkFlow)Activator.CreateInstance(cacheWorkFlow);
            this._duration = duration;
            this._timeEnum = timeEnum;
        }

        public CacheAspect(int duration = 30, TimeEnum timeEnum = TimeEnum.Minute)
        {
            this._cacheWorkFlow = ServiceTool.ServiceProvider.GetService<ICacheWorkFlow>();
            this._duration = duration;
            this._timeEnum = timeEnum;
        }
        public override void Intercept(IInvocation invocation)
        {
            LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();
            StringBuilder logText = new StringBuilder();
            logText.AppendLine("Cache okuma işlemi başladı");
            var returnType = invocation.Method.ReturnType;
            if (returnType != typeof(void) && returnType != typeof(Task))
            {
                string key = this.CreateKey(invocation);
                logText.AppendLine($"Key başarıyla oluşturuldu. Key {key}");

                if (this._cacheWorkFlow.IsAdd(key))
                {
                    logText.AppendLine("Veriler ön bellekten okundu.");
                    if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>) && this._cacheWorkFlow.GetType() == typeof(RedisCacheWorkFlow))
                    {
                        logText.AppendLine("Async metodlarda redis cache kullanılamıyor.");
                    }
                    else if (this._cacheWorkFlow.GetType() == typeof(RedisCacheWorkFlow))
                        invocation.ReturnValue = this._cacheWorkFlow.Get(key, returnType);
                    else
                        invocation.ReturnValue = this._cacheWorkFlow.Get(key);
                    logMessage.InsertLog(logText.ToString(), "Intercept", "CacheAspect.cs");
                    return;
                }
                else
                    logText.AppendLine("Ön bellekte veriler bulunamadı");
            }
            else
                logText.AppendLine("Metod geriye bir değer döndürmüyor. Cache işlemi yapılmadı.");

            logMessage.InsertLog(logText.ToString(), "Intercept", "CacheAspect.cs");
            base.Intercept(invocation);
        }
        public override void OnSuccess(IInvocation invocation)
        {
            StringBuilder logText = new StringBuilder();
            LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();
            logText.AppendLine("Ön belleğe kaydetme işlemi başladı.");
            var returnType = invocation.Method.ReturnType;
            if (returnType != typeof(void) && returnType != typeof(Task))
            {
                string key = this.CreateKey(invocation);
                DateTime cacheDateTime = DateTime.Now.AddMinutes(this._duration * (UInt32)this._timeEnum);

                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>) && this._cacheWorkFlow.GetType() == typeof(RedisCacheWorkFlow))
                    logText.AppendLine("Async metodlarda redis cache kullanılamıyor.");
                else
                    this._cacheWorkFlow.Add(key, invocation.ReturnValue, cacheDateTime);
                logText.AppendLine($"Veriler ön belleğe kaydedildi. Key : {key} CacheDateTime : {cacheDateTime}");
            }
            else
                logText.AppendLine("Metod geriye bir değer döndürmüyor. Cache işlemi yapılmadı.");

            logMessage.InsertLog(logText.ToString(), "OnAfter", "CacheAspect.cs");
            base.OnAfter(invocation);
        }
        private async Task CacheAddAsync<T>(Task<T> task, string key, DateTime cacheDateTime)
        {
            T result = await task.ConfigureAwait(false);
            this._cacheWorkFlow.Add(key, result, cacheDateTime);
        }
        private async Task CacheGetAsync(string key)
        {
            Task task = Task.Run(() => this._cacheWorkFlow.Get(key));
            task.Wait();
            await task.ConfigureAwait(false);
        }
        private string CreateKey(IInvocation invocation)
        {
            string methodFullName = $"{invocation.Method.ReflectedType.Namespace}.{invocation.Method.ReflectedType.Name}.{invocation.Method.Name}";
            List<object> argumentList = invocation.Arguments.ToList();
            return $"{methodFullName}({string.Join(",", argumentList.Select(x => x?.ToString() ?? "<null>"))})";
        }
    }
}
