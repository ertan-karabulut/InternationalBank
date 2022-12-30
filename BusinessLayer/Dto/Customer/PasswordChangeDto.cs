using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Customer
{
    public class PasswordChangeDto
    {
        public string Password { get; set; }
        public string CodeId { get; set; }
        public string CodeCustomerNumber { get; set; }
    }
}
