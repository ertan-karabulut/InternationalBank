using BusinessLayer.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class TokenModelValidation : AbstractValidator<TokenRequestDto>
    {
        public TokenModelValidation()
        {
            RuleFor(x => x.User).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.")
                .MaximumLength(11).WithMessage("{PropertyName} 11 karakterden fazla olamaz.")
                .MinimumLength(8).WithMessage("{PropertyName} 8 karakterden az olamaz.");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.")
                .MaximumLength(6).WithMessage("{PropertyName} 6 karakterden fazla olamaz.")
                .MinimumLength(6).WithMessage("{PropertyName} 6 karakterden az olamaz.");
        }
    }
}
