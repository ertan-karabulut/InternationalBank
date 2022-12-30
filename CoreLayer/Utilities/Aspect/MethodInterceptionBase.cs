using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Aspect
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public abstract class MethodInterceptionBase : Attribute, IInterceptor
    {
        public virtual int Priority { get; set; }
        public virtual void Intercept(IInvocation invocation)
        {
            bool success = true;
            this.OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (System.Exception ex)
            {
                success = false;
                OnException(invocation, ex);
            }
            finally
            {
                if (success)
                    OnSuccess(invocation);
            }
            OnAfter(invocation);
        }

        public virtual void OnBefore(IInvocation invocation) { }
        public virtual void OnAfter(IInvocation invocation) { }
        public virtual void OnException(IInvocation invocation, System.Exception exception)
        {
            throw exception;
        }
        public virtual void OnSuccess(IInvocation invocation) { }
    }
}
