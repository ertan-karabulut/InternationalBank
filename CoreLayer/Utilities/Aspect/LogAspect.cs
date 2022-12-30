using Castle.DynamicProxy;
using CoreLayer.DataAccess.Abstract;
using CoreLayer.Utilities.Helpers;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Logger;
using CoreLayer.Utilities.Messages;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Aspect
{
    public class LogAspect : MethodInterceptionBase
    {
        ILogDto _logDto;
        ILogRepository _logRepository;
        public LogAspect(Type logRepository, Type logDto)
        {
            if (!typeof(ILogRepository).IsAssignableFrom(logRepository))
                throw new System.Exception($"{logRepository.Name} bir ILogger değil!");
            this._logRepository = (ILogRepository)Activator.CreateInstance(logRepository);
            if (!typeof(ILogDto).IsAssignableFrom(logDto))
                throw new System.Exception($"{logDto.Name} bir ILogger değil!");
            this._logDto = (ILogDto)Activator.CreateInstance(logDto);
        }
        public override void OnBefore(IInvocation invocation)
        {
            HelperWorkFlow helperWorkFlow = new HelperWorkFlow();
            _logDto.Message = JsonConvert.SerializeObject(invocation.Arguments);
            _logDto.Method = $"{invocation.Method.Name} OnBefore";
            _logDto.Page = $"{invocation.Method.ReflectedType.Namespace}.{invocation.Method.ReflectedType.Name}";
            _logDto.Token = helperWorkFlow.GetToken();
            _logDto.Ip = helperWorkFlow.GetIpAddress();
            this._logRepository.AddLog(_logDto);

            base.OnBefore(invocation);
        }

        public override void OnSuccess(IInvocation invocation)
        {
            HelperWorkFlow helperWorkFlow = new HelperWorkFlow();
            LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();
            _logDto.Message = $"Inline log : {logMessage.GetLogText()} {invocation.ReturnValue}";
            _logDto.Method = $"{invocation.Method.Name} OnAfter";
            _logDto.Page = $"{invocation.Method.ReflectedType.Namespace}.{invocation.Method.ReflectedType.Name}";
            _logDto.Token = helperWorkFlow.GetToken();
            _logDto.Ip = helperWorkFlow.GetIpAddress();
            this._logRepository.AddLog(_logDto);

            base.OnSuccess(invocation);
        }

        public override void OnException(IInvocation invocation, System.Exception exception)
        {
            HelperWorkFlow helperWorkFlow = new HelperWorkFlow();
            LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();

            _logDto.Message = $"{logMessage.GetLogText()} Hata : {exception.ToString()}";
            _logDto.Method = $"{invocation.Method.Name} OnAfter";
            _logDto.Page = $"{invocation.Method.ReflectedType.Namespace}.{invocation.Method.ReflectedType.Name}";
            _logDto.Token = helperWorkFlow.GetToken();
            _logDto.Ip = helperWorkFlow.GetIpAddress();
            this._logRepository.AddLog(_logDto);
            throw exception;
        }
    }
}
