using CoreLayer.DataAccess.Abstract;
using CoreLayer.DataAccess.Concrete;
using CoreLayer.DataAccess.Concrete.Repository;
using CoreLayer.Utilities.Ioc;
using EntityLayer.Models.EntityFremework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    abstract public class RepositoryBase<TEntity> : EfEntityRepository<TEntity,MyBankContext> where TEntity : class, new()
    {
        protected RepositoryBase(MyBankContext context) : base(context)
        {
        }
    }
}
