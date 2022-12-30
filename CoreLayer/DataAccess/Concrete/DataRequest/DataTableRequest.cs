using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.DataRequest
{
    public class DataTableRequest : DataRequestBase
    {
        public DataTableRequest()
        {
            Order = new OrderModel();
        }
        public int Skip { get; set; }
        public int Take { get; set; }
        public OrderModel Order { get; set; }
    }
    public class OrderModel
    {
        public string Column { get; set; }
        public string Short { get; set; }
    }
}
