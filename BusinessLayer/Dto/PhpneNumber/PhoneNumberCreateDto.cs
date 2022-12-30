using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.PhpneNumber
{
    public class PhoneNumberCreateDto
    {
        public int CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string NumberName { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
