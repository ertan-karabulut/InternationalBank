using BusinessLayer.Dto.Adress;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.AdressValidation
{
    public class CreateAdressValidation : AbstractValidator<AdressCreateDto>
    {
        public CreateAdressValidation()
        {
            RuleFor(x => x.DistrictId).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CityId).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.CountryId).GreaterThan(0).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");

            RuleFor(x => x.AdressDetail).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").MaximumLength(100).WithMessage("{PropertyName} 100 karakterden fazla olamaz.");

            RuleFor(x => x.AdressName).MaximumLength(15).WithMessage("{PropertyName} 15 karakterden fazla olamaz.");
        }
    }
}
