using CoreLayer.BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short Gender { get; set; }
        public string Photo { get; set; }
    }
}
