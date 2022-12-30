namespace WebBlazor.Models.DataModel
{
    public class DataTableRequest : DataRequestBase
    {
        public DataTableRequest()
        {
            Order = new OrderModel();
        }
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
        public OrderModel Order { get; set; }
    }
    public class OrderModel
    {
        public string Column { get; set; }
        public string Short { get; set; }
    }
}
