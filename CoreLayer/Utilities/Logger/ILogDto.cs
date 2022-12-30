using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Logger
{
    public interface ILogDto
    {
        string Token { get; set; }
        string Message { get; set; }
        string Page { get; set; }
        string Method { get; set; }
        DateTime CreateDate { get; set; }
        string Ip { get; set; }
    }
}
