using System;

namespace WebBlazor.Models.Mail
{
    public class MailDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string EMail { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
