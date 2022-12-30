using System;
using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models.Address
{
    public class AdressUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id alanı sıfıdan büyük olmalıdır.")]
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId alanı sıfıdan büyük olmalıdır.")]
        public int CustomerId { get; set; }
        public string AdressName { get; set; }
        [Required(ErrorMessage = "Adres alanı boş bırakılamaz."), MaxLength(100,ErrorMessage = "Adres alanı 100 karakterden fazla olamaz.")]
        public string AdressDetail { get; set; }
        [Required(ErrorMessage = "İl alanı boş bırakılamaz.")]
        public int? CityId { get; set; }
        [Required(ErrorMessage = "İlçe alanı boş bırakılamaz.")]
        public int? DistrictId { get; set; }
        [Required(ErrorMessage = "Ülke alanı boş bırakılamaz.")]
        public int? CountryId { get; set; }
        public DateTime? DomicileStartDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool? IsActive { get; set; }
    }
}
