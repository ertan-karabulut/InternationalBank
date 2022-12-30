using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Account
{
    public class AccountCreatDto
    {
        public int BranchId { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public int TypeId { get; set; }
        public int CurrencyUnitId { get; set; }
    }

    public class AccountCreatDtoList
    {
        public AccountCreatDtoList()
        {
            AccountList = new List<AccountCreatDto>();
        }
        public List<AccountCreatDto> AccountList { get; set; }
    }
}
