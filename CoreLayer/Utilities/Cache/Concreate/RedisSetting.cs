using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Cache.Concreate
{
    public class RedisSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public int Db { get; set; }
    }
}
