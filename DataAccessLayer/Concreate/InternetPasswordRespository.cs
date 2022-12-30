using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    public class InternetPasswordRespository : RepositoryBase<InternetPassword>, IInternetPasswordRespository
    {
        public InternetPasswordRespository(MyBankContext context) : base(context)
        {
        }
    }
}
