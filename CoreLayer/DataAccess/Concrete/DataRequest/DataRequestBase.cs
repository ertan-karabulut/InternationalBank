using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.DataRequest
{
    public class DataRequestBase
    {
        public DataRequestBase()
        {
            Filter = new List<NameValuePair>();
        }

        public List<NameValuePair> Filter { get; set; }
    }

    public class NameValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
