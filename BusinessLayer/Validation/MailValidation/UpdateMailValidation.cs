using BusinessLayer.Dto.Mail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.MailValidation
{
    public class UpdateMailValidation : AbstractValidator<MailUpdateDto>
    {
        public UpdateMailValidation()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.EMail).Must(x => string.Empty != x).WithMessage("{PropertyName} boş bırakılamaz.").Must(x => x != null && x.Count() > 40 ? false : true).WithMessage("{PropertyName} 40 karakterden fazla olamaz.");
        }
    }
}
