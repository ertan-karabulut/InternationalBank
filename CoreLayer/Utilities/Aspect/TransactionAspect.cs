using Castle.DynamicProxy;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CoreLayer.Utilities.Aspect
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAspect : MethodInterceptionBase
    {
        public override int Priority { get => int.MaxValue; set => base.Priority = int.MaxValue; }
        public override void Intercept(IInvocation invocation)
        {
            LogMessage logMessage = ServiceTool.ServiceProvider.GetService<LogMessage>();
            var returnType = invocation.Method.ReturnType;
            if (returnType != typeof(void))
            {
                if ((returnType == typeof(Task)) || returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    logMessage.InsertLog("TransactionAspect async metodlarda uygulanmamaktadır.", "Intercept", "TransactionAspect");
                    throw new System.Exception("TransactionAspect async metodlarda uygulanmamaktadır.");
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        try
                        {
                            invocation.Proceed();
                            scope.Complete();
                        }
                        catch (System.Exception ex)
                        {
                            scope.Dispose();
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    try
                    {
                        invocation.Proceed();
                        scope.Complete();
                    }
                    catch (System.Exception ex)
                    {
                        scope.Dispose();
                        throw ex;
                    }
                }
            }
        }
    }
}
