using System;
using System.Text;
using WebBlazor.Enums;

namespace WebBlazor.Models.Account
{
    public class AccountHistoryDto
    {
        public int AccountId { get; set; }
        public decimal NowBalance { get; set; }
        public decimal BeforeBalance { get; set; }
        public decimal Amount { get; set; }
        public string Explanation { get; set; }
        public DateTime? CreateDate { get ; set; }
    }
}
