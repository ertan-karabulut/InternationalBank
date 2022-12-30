using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlazor.Services.Abstract
{
    interface IServiceOperations
    {
        Task<T> Get<T>(string url, object data = null);
        Task<T> Post<T>(string url, object data = null);
    }
}
