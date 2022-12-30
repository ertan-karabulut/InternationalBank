using System.Collections.Generic;

namespace WebBlazor.Models.DataModel
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
