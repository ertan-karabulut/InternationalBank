using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Aspect
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBase>(true).ToList();
            if (!typeof(IValidator).IsAssignableFrom(type))
            {
                Type[] types = new Type[method.GetParameters().Count()];
                for (int i = 0; i < method.GetParameters().Count(); i++)
                {
                    types[i] = method.GetParameters()[i].ParameterType;
                }
                var methodAttributes = type.GetMethod(method.Name, types).GetCustomAttributes<MethodInterceptionBase>(true);

                classAttributes.AddRange(methodAttributes);
            }

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
