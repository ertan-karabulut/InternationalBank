
using CoreLayer.Utilities.Result.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.Repository
{
    public class EfEntityRepository<TEntity, TContext> where TEntity : class, new() where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public EfEntityRepository(TContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<TEntity>();
        }
        /// <summary>
        /// Sorguyu döner
        /// </summary>
        virtual public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            return this._dbSet.AsTracking(isTracking).Where(predicate);
        }
        /// <summary>
        /// Liste şeklinde gönderilen veri tabanı nesnesini ekler.
        /// </summary>
        virtual public async Task<Result<List<TEntity>>> AddAsync(List<TEntity> entity)
        {
            Result<List<TEntity>> result = new Result<List<TEntity>>();
            await this._dbSet.AddRangeAsync(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }
        /// <summary>
        /// Liste şeklinde gönderilen veri tabanı nesnesini ekler.
        /// </summary>
        virtual public Result<List<TEntity>> Add(List<TEntity> entity)
        {
            Result<List<TEntity>> result = new Result<List<TEntity>>();
            this._dbSet.AddRange(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }

        /// <summary>
        /// Veri tabanı nesnesini ekler.
        /// </summary>
        virtual public async Task<Result<TEntity>> AddAsync(TEntity entity)
        {
            Result<TEntity> result = new Result<TEntity>();
            await this._dbSet.AddAsync(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }
        /// <summary>
        /// Veri tabanı nesnesini ekler.
        /// </summary>
        virtual public Result<TEntity> Add(TEntity entity)
        {
            Result<TEntity> result = new Result<TEntity>();
            this._dbSet.Add(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }

        /// <summary>
        /// Veri tabanı nesnesini günceller.
        /// </summary>
        virtual public Result Update(TEntity entity)
        {
            Result result = new Result();
            this._dbSet.Update(entity);
            result.SetTrue();
            return result;
        }
        /// <summary>
        /// Liste şeklinde gönderilen veri tabanı nesnesini günceller.
        /// </summary>
        virtual public Result Update(List<TEntity> entity)
        {
            Result result = new Result();
            this._dbSet.UpdateRange(entity);
            result.SetTrue();
            return result;
        }
        /// <summary>
        /// Veritabanı nesnesini siler.
        /// </summary>
        virtual public Result Delete(TEntity entity)
        {
            Result result = new Result();
            this._dbSet.Remove(entity);
            result.SetTrue();
            return result;
        }
        /// <summary>
        /// Liste şeklinde gönderilen veri tabanı nesnesini siler.
        /// </summary>
        virtual public Result Delete(List<TEntity> entity)
        {
            Result result = new Result();
            this._dbSet.RemoveRange(entity);
            result.SetTrue();
            return result;
        }
    }
}
