using BusinessLayer.Dto.Account;
using BusinessLayer.Dto.Adress;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.AdressValidation
{
    public class UpdateAdressValidation : AbstractValidator<AdressUpdateDto>
    {
        public UpdateAdressValidation()
        {
            RuleFor(x=>x.Id).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.DistrictId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CityId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CountryId).Must(x => x.HasValue ? x.Value > 0 : true).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.AdressDetail).Must(x=> string.Empty != x).WithMessage("{PropertyName} boş bırakılamaz.").Must(x => x != null && x.Count() > 100 ? false : true).WithMessage("{PropertyName} 100 karakterden fazla olamaz.");

            RuleFor(x => x.AdressName).MaximumLength(15).WithMessage("{PropertyName} 15 karakterden fazla olamaz.");
        }
    }
}
