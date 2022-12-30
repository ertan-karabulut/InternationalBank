using System;
using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models.Mail
{
    public class MailCreateDto
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "E-Posta alanı boş bırakılamaz."), MaxLength(40, ErrorMessage = "E-Posta alanı 40 karakterden fazla olamaz."), EmailAddress(ErrorMessage = "Lütfen geçerli bir mail adresi giriniz.")]
        public string EMail { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
