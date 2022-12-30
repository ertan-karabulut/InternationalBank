
using CoreLayer.Utilities.Result.Concreate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity, TContext> where TEntity : class where TContext : DbContext, new()
    {
        #region Listeleme metodları
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        #endregion

        #region Ekleme metodları async
        Task<Result<List<TEntity>>> AddAsync(List<TEntity> entity);
        Task<Result<TEntity>> AddAsync(TEntity entity);
        #endregion

        #region Ekleme metodları
        Result<List<TEntity>> Add(List<TEntity> entity);
        Result<TEntity> Add(TEntity entity);
        #endregion

        #region Güncelleme metodları
        Result Update(TEntity entity);
        Result Update(List<TEntity> entity);
        #endregion

        #region Silme medotları
        Result Delete(List<TEntity> entity);
        Result Delete(TEntity entity);
        #endregion
    }
}
