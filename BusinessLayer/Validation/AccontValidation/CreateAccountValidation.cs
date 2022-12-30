using BusinessLayer.Dto.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.AccontValidation
{
    public class CreateAccountValidation : AbstractValidator<AccountCreatDto>
    {
        public CreateAccountValidation()
        {
            RuleFor(x => x.BranchId).
                GreaterThan(0).WithMessage("{PropertyName}  0'dan büyük olmalıdır.");
            RuleFor(x => x.CustomerId).
                GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
            RuleFor(x => x.TypeId).
                GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
            RuleFor(x => x.CurrencyUnitId).
                GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
            RuleFor(x => x.AccountName).Must(x => string.Empty != x).WithMessage("{PropertyName} boş bırakılamaz.").Must(x=> x != null && x.Count() > 30 ? false : true).WithMessage("{PropertyName} 30 karakterden fazla olamaz.");
        }
    }

    public class CreateAccountListValidation : AbstractValidator<AccountCreatDtoList>
    {
        public CreateAccountListValidation()
        {
            RuleFor(x=> x.AccountList).Must(x=> x.Count() > 0).WithMessage("Lütfen hesap bilgisini giriniz.");
            RuleForEach(x => x.AccountList)
                .SetValidator(new CreateAccountValidation());
        }
    }
}
