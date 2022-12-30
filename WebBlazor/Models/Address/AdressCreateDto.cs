using System;
using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models.Address
{
    public class AdressCreateDto
    {
        public int CustomerId { get; set; }
        public string AdressName { get; set; }
        [Required(ErrorMessage = "Adres alanı boş bırakılamaz."), MaxLength(100, ErrorMessage = "Adres alanı 100 karakterden fazla olamaz.")]
        public string AdressDetail { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "İl alanı boş bırakılamaz.")]
        public int CityId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "İlçe alanı boş bırakılamaz.")]
        public int DistrictId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Ülke alanı boş bırakılamaz.")]
        public int CountryId { get; set; }
        public DateTime? DomicileStartDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
