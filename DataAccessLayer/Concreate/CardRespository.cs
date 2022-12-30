using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    public class CardRespository : RepositoryBase<Card>, ICardRespository
    {
        public CardRespository(MyBankContext context) : base(context)
        {
        }
    }
}
