using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Enum
{
    public enum FileSizesEnum : ulong
    {
        Byte = 1,
        Kb = 1024,
        Mb = 1048576,
        Gb = 1073741824,
        Tb = 1099511627776,
        Pb = 1125899906842624
    }
}
