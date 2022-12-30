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
    public class PasswordChangeValidation : AbstractValidator<PasswordChangeDto>
    {
        public PasswordChangeValidation()
        {
            RuleFor(x => x.CodeId).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.");
            RuleFor(x => x.CodeCustomerNumber).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.")
                .MaximumLength(6).WithMessage("{PropertyName} 6 karakterden fazla olamaz.")
                .MinimumLength(6).WithMessage("{PropertyName} 6 karakterden az olamaz.");
        }
    }
}
