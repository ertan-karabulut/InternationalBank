using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Result.Abstract;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Result.Concreate
{
    public static class ResultInjection
    {
        public static IResult<T> Result<T>()
        {
            IResult<T> result = ServiceTool.ServiceProvider.GetService<IResult<T>>();
            return result;
        }
        public static IResult Result()
        {
            return ServiceTool.ServiceProvider.GetService<IResult>();
        }
    }
}
