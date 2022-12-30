using BusinessLayer.Dto;
using BusinessLayer.Dto.Customer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class PasswordConfirmValidation : AbstractValidator<PasswordConfirmDto>
    {
        public PasswordConfirmValidation()
        {
            RuleFor(x => x.User).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.")
                .MaximumLength(11).WithMessage("{PropertyName} 11 karakterden fazla olamaz.")
                .MinimumLength(8).WithMessage("{PropertyName} 8 karakterden az olamaz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").MaximumLength(10).WithMessage("{PropertyName} 10 karakterden fazla olamaz.").MinimumLength(10).WithMessage("{PropertyName} 10 karakterden az olamaz.");
        }
    }
}
