using System;

namespace WebBlazor.Models.Phone
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string NumberName { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
