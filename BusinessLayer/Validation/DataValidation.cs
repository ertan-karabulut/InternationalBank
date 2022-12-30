using CoreLayer.DataAccess.Concrete.DataRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class DataValidation : AbstractValidator<DataRequest>
    {
        public DataValidation()
        {
            RuleFor(x => x.Filter).NotEmpty().WithMessage("{PropertyName} boş olamaz.");
        }
    }
}
