using System.Collections.Generic;

namespace WebBlazor.Models.Address
{
    public class AdressDetailDto
    {
        public AdressDetailDto()
        {
            Adress = new AdressDto();
            CountrySelectList = new List<SelectListItemModel>();
            CitySelectList = new List<SelectListItemModel>();
            DistrictSelectList = new List<SelectListItemModel>();
        }

        public AdressDto Adress { get; set; }
        public List<SelectListItemModel> CountrySelectList { get; set; }
        public List<SelectListItemModel> CitySelectList { get; set; }
        public List<SelectListItemModel> DistrictSelectList { get; internal set; }
    }
}
