using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.Branch
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string BranchNumber { get; set; }
        public string BranchName { get; set; }
        public string BranchAdress { get; set; }
        public int? DistrictId { get; set; }
        public int CityId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
    }
}
