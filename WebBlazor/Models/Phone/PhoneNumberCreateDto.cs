using System;
using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models.Phone
{
    public class PhoneNumberCreateDto
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz."), MaxLength(10, ErrorMessage = "Telefon numarası alanı 10 karakterden fazla olamaz."), MinLength(10, ErrorMessage = "Telefon numarası alanı 10 karakterden az olamaz.")]
        public string PhoneNumber { get; set; }
        [MaxLength(10, ErrorMessage = "Telefon adı alanı 10 karakterden fazla olamaz.")]
        public string NumberName { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
