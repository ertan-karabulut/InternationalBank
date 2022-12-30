using BusinessLayer.Dto.Mail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.MailValidation
{
    public class CreateMailValidation : AbstractValidator<MailCreateDto>
    {
        public CreateMailValidation()
        {
            RuleFor(x => x.EMail).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").MaximumLength(40).WithMessage("{PropertyName} 40 karakterden fazla olamaz.");
        }
    }
}
