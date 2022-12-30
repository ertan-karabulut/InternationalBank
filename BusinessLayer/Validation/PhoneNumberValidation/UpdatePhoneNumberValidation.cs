using BusinessLayer.Dto.PhpneNumber;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.PhoneNumberValidation
{
    public class UpdatePhoneNumberValidation : AbstractValidator<PhoneNumberUpdateDto>
    {
        public UpdatePhoneNumberValidation()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.PhoneNumber).Must(x => x != null && x.Count() < 10 ? false : true).WithMessage("{PropertyName} 10 karakterden az olamaz.").Must(x => x != null && x.Count() > 10 ? false : true).WithMessage("{PropertyName} 10 karakterden fazla olamaz.");

            RuleFor(x => x.NumberName).MaximumLength(10).WithMessage("{PropertyName} 10 karakterden fazla olamaz.");
        }
    }
}
