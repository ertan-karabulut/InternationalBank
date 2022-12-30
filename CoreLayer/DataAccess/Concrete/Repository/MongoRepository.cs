using CoreLayer.DataAccess.Abstract;

using CoreLayer.Utilities.Result.Concreate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.Repository
{
    public class MongoRepository<TCollection> : IMongoRepository<TCollection>
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TCollection> _collection;

        public MongoRepository(IDatabaseSttings databaseSttings)
        {
            this._context = new MongoDbContext(databaseSttings);
            this._collection = this._context.GetCollection<TCollection>();
        }

        public IQueryable<TCollection> Get()
        {
            return this._collection.AsQueryable();
        }

        public async Task<Result<TCollection>> AddAsync(TCollection entity)
        {
            var result = new Result<TCollection>();
            await this._collection.InsertOneAsync(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }

        public Result<TCollection> Add(TCollection entity)
        {
            var result = new Result<TCollection>();
            this._collection.InsertOne(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }

        public async Task<Result<List<TCollection>>> AddAsync(List<TCollection> entity)
        {
            var result = new Result<List<TCollection>>();
            await this._collection.InsertManyAsync(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }

        public Result<List<TCollection>> Add(List<TCollection> entity)
        {
            var result = new Result<List<TCollection>>();
            this._collection.InsertMany(entity);
            result.SetTrue();
            result.ResultObje = entity;
            return result;
        }
    }
}
