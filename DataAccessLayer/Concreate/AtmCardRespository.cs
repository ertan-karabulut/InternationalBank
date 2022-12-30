using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    public class AtmCardRespository : RepositoryBase<AtmCard>, IAtmCardRespository
    {
        public AtmCardRespository(MyBankContext context) : base(context)
        {
        }
    }
}
