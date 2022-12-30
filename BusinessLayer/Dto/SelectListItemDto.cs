using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class SelectListItemDto
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public bool IsDisabled { get; set; }
    }
}
