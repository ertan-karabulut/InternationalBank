
using CoreLayer.Utilities.Result.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Abstract
{
    public interface IMongoRepository<TCollection>
    {
        IQueryable<TCollection> Get();
        Task<Result<TCollection>> AddAsync(TCollection entity);
        Result<TCollection> Add(TCollection entity);
        Task<Result<List<TCollection>>> AddAsync(List<TCollection> entity);
        Result<List<TCollection>> Add(List<TCollection> entity);
    }
}
