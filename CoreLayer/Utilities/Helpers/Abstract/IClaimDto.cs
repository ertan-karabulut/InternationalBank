using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Helpers.Abstract
{
    public interface IClaimDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<string> RoleList { get; set; }
        public string Photo { get; set; }
        public string CustomerNumber { get; set; }
    }
}
