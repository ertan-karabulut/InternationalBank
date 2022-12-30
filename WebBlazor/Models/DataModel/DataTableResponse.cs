using System.Collections.Generic;

namespace WebBlazor.Models.DataModel
{
    public class DataTableResponse<T>
    {
        public List<T> Data { get; set; }
        public long TotalCount { get; set; }
    }
}
