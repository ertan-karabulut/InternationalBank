using CoreLayer.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.Repository
{
    public class DatabaseSttings : IDatabaseSttings
    {
        public string InformationCollectionName { get; set; }
        public string ExceptionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
