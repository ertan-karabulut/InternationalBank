using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlazor.Models
{
    class PasswordConfirmModel
    {
        [Required(ErrorMessage = "Müşteri / T.C. Kimlik Numarası boş bırakılamaz.")]
        [MaxLength(11,ErrorMessage = "Müşteri / T.C. Kimlik Numarası 11 karakterden fazla olamaz.")]
        [MinLength(8,ErrorMessage = "Müşteri / T.C. Kimlik Numarası 8 karakterden az olamaz.")]
        public string User { get; set; }
        [Required(ErrorMessage = "Telefon numarası boş bırakılamaz.")]
        [MaxLength(10, ErrorMessage = "Telefon numarası 10 karakterden fazla olamaz.")]
        [MinLength(10, ErrorMessage = "Telefon numarası 10 karakterden az olamaz.")]
        public string PhoneNumber { get; set; }
    }
}
