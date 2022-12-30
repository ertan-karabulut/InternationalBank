using CoreLayer.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWorkBase
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;
        public UnitOfWorkBase(DbContext context)
        {
            this._context = context;
        }
        public void BeginTransaction()
        {
            if (this._transaction == null)
                this._transaction = this._context.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            if (this._transaction == null)
                this._transaction = await this._context.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            if (this._transaction != null)
                this._transaction.Commit();
        }

        public async Task CommitAsync()
        {
            if (this._transaction != null)
                await this._transaction.CommitAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this._disposed = true;
        }

        public void Rollback()
        {
            if (this._transaction != null)
                this._transaction.Rollback();
        }

        public async Task RollbackAsync()
        {
            if (this._transaction != null)
                await this._transaction.RollbackAsync();
        }

        public bool SaveChanges()
        {
            return this._context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync() > 0;
        }
    }
}
