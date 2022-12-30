using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Exception
{
    public class BaseException : System.Exception
    {
        public BaseException() { }
        public BaseException(string message) : base(message: message) { }
    }
}
