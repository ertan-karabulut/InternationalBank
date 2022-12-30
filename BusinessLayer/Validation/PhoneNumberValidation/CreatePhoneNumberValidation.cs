using BusinessLayer.Dto.PhpneNumber;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.PhoneNumberValidation
{
    public class CreatePhoneNumberValidation : AbstractValidator<PhoneNumberCreateDto>
    {
        public CreatePhoneNumberValidation()
        {
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("{PropertyName} boş bırakılamaz.").MaximumLength(10).WithMessage("{PropertyName} 10 karakterden fazla olamaz.").MinimumLength(10).WithMessage("{PropertyName} 10 karakterden az olamaz.");

            RuleFor(x => x.NumberName).MaximumLength(10).WithMessage("{PropertyName} 10 karakterden fazla olamaz.");
        }
    }
}
