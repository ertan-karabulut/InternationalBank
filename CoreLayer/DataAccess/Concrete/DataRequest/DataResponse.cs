using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.DataRequest
{
    public class DataResponse<T>
    {
        public T Data { get; set; }
    }
}
