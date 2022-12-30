using Castle.DynamicProxy;
using CoreLayer.Utilities.Cache.Abstract;
using CoreLayer.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Aspect
{
    public class RemoveCacheAspect : MethodInterceptionBase
    {
        private ICacheWorkFlow _cacheWorkFlow;
        private string _pattern;
        public RemoveCacheAspect(string pattern = "")
        {
            this._cacheWorkFlow = ServiceTool.ServiceProvider.GetService<ICacheWorkFlow>();
            this._pattern = pattern;
        }

        public RemoveCacheAspect(Type cacheType, string pattern = "") : this(pattern)
        {
            if (!typeof(ICacheWorkFlow).IsAssignableFrom(cacheType))
                throw new System.Exception("Hatalı cache belirtildi.");
            this._cacheWorkFlow = (ICacheWorkFlow)Activator.CreateInstance(cacheType);
        }

        public override void OnSuccess(IInvocation invocation)
        {
            this._cacheWorkFlow.RemoveByPattern(this._pattern);
            base.OnSuccess(invocation);
        }
    }
}
