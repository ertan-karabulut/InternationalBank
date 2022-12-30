using System.ComponentModel.DataAnnotations;

namespace WebBlazor.Models
{
    public class PasswordChangeModel
    {
        [Required(ErrorMessage = "Parola boş bırakılamaz.")]
        [MaxLength(6, ErrorMessage = "Parola 6 karakterden fazla olamaz.")]
        [MinLength(6, ErrorMessage = "Parola 6 karakterden az olamaz.")]
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string CodeId { get; set; }
        public string CodeCustomerNumber { get; set; }
    }
}
