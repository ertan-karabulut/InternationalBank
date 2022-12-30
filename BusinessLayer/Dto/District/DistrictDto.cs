using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto.District
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int? CityId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
