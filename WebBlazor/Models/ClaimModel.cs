using System.Collections.Generic;

namespace WebBlazor.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> RoleList { get; set; }
        public string Photo { get; set; }
        public string CustomerNumber { get; set; }
    }
}
