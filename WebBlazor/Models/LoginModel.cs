using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebBlazor.Models
{
    class LoginModel
    {
        [Required(ErrorMessage = "Müşteri / T.C. Kimlik Numarası boş bırakılamaz.")]
        [MaxLength(11,ErrorMessage = "Müşteri / T.C. Kimlik Numarası 11 karakterden fazla olamaz.")]
        [MinLength(8,ErrorMessage = "Müşteri / T.C. Kimlik Numarası 8 karakterden az olamaz.")]
        public string User { get; set; }
        [Required(ErrorMessage = "Parola boş bırakılamaz.")]
        [MaxLength(6, ErrorMessage = "Parola 6 karakterden fazla olamaz.")]
        [MinLength(6, ErrorMessage = "Parola 6 karakterden az olamaz.")]
        public string Password { get; set; }
    }
}
