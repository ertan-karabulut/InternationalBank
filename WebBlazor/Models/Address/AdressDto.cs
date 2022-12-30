using System;

namespace WebBlazor.Models.Address
{
    public class AdressDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AdressName { get; set; }
        public string AdressDetail { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int CountryId { get; set; }
        public DateTime? DomicileStartDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }

        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string CountryName { get; set; }
    }
}
