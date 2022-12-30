using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Abstract
{
    public interface IUnitOfWorkBase : IDisposable
    {
        void Rollback();
        Task RollbackAsync();
        void Commit();
        Task CommitAsync();
        void BeginTransaction();
        Task BeginTransactionAsync();
        bool SaveChanges();
        Task<bool> SaveChangesAsync();

    }
}
