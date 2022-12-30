using System;

namespace WebBlazor.Models.Account
{
    public class AccountDetailDto
    {
        public int Id { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerNumber { get; set; }
        public string Account { get; set; }
        public string Iban { get; set; }
        public DateTime? AccountOpenDate { get; set; }
        public string AccountType { get; set; }
        public string CurrencyCode { get; set; }
        public string Balance { get; set; }
        public string AvailableBalance { get; set; }
        public string BranchName { get; set; }
        public DateTime? BalanceDate { get; set; }
    }
}
