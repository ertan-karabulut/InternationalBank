using CoreLayer.DataAccess.Abstract;
using CoreLayer.DataAccess.Concrete.Repository;
using EntityLayer.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    public class ExceptionLogRepository : MongoLogRepository<ExceptionLog>, ILogRepository
    {
    }
}
