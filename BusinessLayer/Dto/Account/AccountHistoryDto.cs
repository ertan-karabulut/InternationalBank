using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Account
{
    public class AccountHistoryDto
    {
        public int AccountId { get; set; }
        public decimal NowBalance { get; set; }
        public decimal BeforeBalance { get; set; }
        public decimal Amount { get; set; }
        public string Explanation { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
