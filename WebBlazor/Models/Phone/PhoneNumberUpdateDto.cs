using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models.Phone
{
    public class PhoneNumberUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id alanı sıfıdan büyük olmalıdır.")]
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId alanı sıfıdan büyük olmalıdır.")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz."), MaxLength(10, ErrorMessage = "Telefon numarası alanı 10 karakterden fazla olamaz."), MinLength(10, ErrorMessage = "Telefon numarası alanı 10 karakterden az olamaz."), Phone(ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
        [MaxLength(10, ErrorMessage = "Telefon adı alanı 10 karakterden fazla olamaz.")]
        public string NumberName { get; set; }
        public bool IsFavorite { get; set; }
        public bool? IsActive { get; set; }
    }
}
