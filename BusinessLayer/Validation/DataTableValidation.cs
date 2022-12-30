using CoreLayer.DataAccess.Concrete.DataRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class DataTableValidation : AbstractValidator<DataTableRequest>
    {
        public DataTableValidation()
        {
            RuleFor(x => x.Take).Must(x => x > 0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
        }
    }
}
