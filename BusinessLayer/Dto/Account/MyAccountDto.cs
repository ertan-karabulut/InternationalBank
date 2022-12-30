using CoreLayer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Account
{
    public class MyAccountDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string BalanceStr { get; set; }
        public decimal AvailableBalance { get; set; }
        public string AvailableBalanceStr { get; set; }
        public string Iban { get; set; }
        public string ShortName { get; set; }
    }
}
