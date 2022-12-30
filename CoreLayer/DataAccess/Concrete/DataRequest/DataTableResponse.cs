using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.DataRequest
{
    public class DataTableResponse<T>
    {
        public List<T> Data { get; set; }
        public long TotalCount { get; set; }
    }
}
