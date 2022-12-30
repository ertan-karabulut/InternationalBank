using System;

namespace WebBlazor.Models
{
    public class PaginationModel
    {
        public int Skip { get; set; }
        public int PageNumber { get; set; }
        public bool Active { get; set; }
        public bool IsNextFivePage { get; set; }
        public bool IsBackFivePage { get; set; }
    }
}
