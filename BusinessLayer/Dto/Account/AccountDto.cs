using CoreLayer.BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string AccountName { get; set; }
        public int CustomerId { get; set; }
        public string Iban { get; set; }
        public string AccountNumber { get; set; }
        public int TypeId { get; set; }
        public int CurrencyUnitId { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class AccountDtoList
    {
        public AccountDtoList()
        {
            AccountDto = new List<AccountDto>();
        }
        public List<AccountDto> AccountDto { get; set; }
    }
}
