using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Account
{
    public class AccountUpdateDto
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public string AccountName { get; set; }
        public int? CustomerId { get; set; }
        public int? TypeId { get; set; }
        public int? CurrencyUnitId { get; set; }
        public string Iban { get; set; }
        public string AccountNumber { get; set; }
        public bool? IsActive { get; set; }
    }

    public class AccountUpdateDtoList
    {
        public AccountUpdateDtoList()
        {
            AccountList = new List<AccountUpdateDto>();
        }
        public List<AccountUpdateDto> AccountList { get; set; }
    }
}
