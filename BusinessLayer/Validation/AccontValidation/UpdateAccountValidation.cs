using BusinessLayer.Dto.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.AccontValidation
{
    public class UpdateAccountValidation : AbstractValidator<AccountUpdateDto>
    {
        public UpdateAccountValidation()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName}  0'dan büyük olmalıdır.");

            RuleFor(x => x.BranchId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName}  0'dan büyük olmalıdır.");

            RuleFor(x => x.CustomerId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName}  0'dan büyük olmalıdır.");

            RuleFor(x => x.TypeId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName}  0'dan büyük olmalıdır.");

            RuleFor(x => x.CurrencyUnitId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName}  0'dan büyük olmalıdır.");

            RuleFor(x => x.Iban).Must(x => x != null && x.Count() > 24 ? false : true).WithMessage("{PropertyName} 25 karakterden fazla olamaz.")
                .Must(x => x != null && x.Count() < 24 ? false : true).WithMessage("{PropertyName} 24 karakterden az olamaz.");

            RuleFor(x => x.AccountNumber).Must(x => x != null && x.Count() > 7 ? false :true).WithMessage("{PropertyName} 7 karakterden fazla olamaz.").Must(x => x != null && x.Count() < 7 ? false : true).WithMessage("{PropertyName} 7 karakterden az olamaz.");

            RuleFor(x => x.AccountName).MaximumLength(30).WithMessage("{PropertyName} 30 karakterden fazla olamaz.");
        }
    }
    public class UpdateAccountListValidation : AbstractValidator<AccountUpdateDtoList>
    {
        public UpdateAccountListValidation()
        {
            RuleFor(x => x.AccountList).Must(x => x.Count() > 0).WithMessage("Lütfen hesap bilgisini giriniz.");
            RuleForEach(x => x.AccountList)
                .SetValidator(new UpdateAccountValidation());
        }
    }
}
