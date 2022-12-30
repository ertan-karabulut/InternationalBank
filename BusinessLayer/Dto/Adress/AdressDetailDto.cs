using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Adress
{
    public class AdressDetailDto
    {
        public AdressDetailDto()
        {
            Adress = new AdressDto();
            CountrySelectList = new List<SelectListItemDto>();
            CitySelectList = new List<SelectListItemDto>();
            DistrictSelectList = new List<SelectListItemDto>();
        }

        public AdressDto Adress { get; set; }
        public List<SelectListItemDto> CountrySelectList { get; set; }
        public List<SelectListItemDto> CitySelectList { get; set; }
        public List<SelectListItemDto> DistrictSelectList { get; internal set; }
    }
}
