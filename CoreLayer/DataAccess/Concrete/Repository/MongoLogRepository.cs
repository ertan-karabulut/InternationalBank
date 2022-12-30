using CoreLayer.DataAccess.Abstract;
using CoreLayer.Utilities.Ioc;
using CoreLayer.Utilities.Logger;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.Repository
{
    public class MongoLogRepository<TCollection> : ILogRepository where TCollection : ILogDto
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TCollection> _collection;

        public MongoLogRepository()
        {
            IDatabaseSttings dbSettings = ServiceTool.ServiceProvider.GetService<IDatabaseSttings>();
            this._context = new MongoDbContext(dbSettings);
            this._collection = this._context.GetCollection<TCollection>();
        }

        public void AddLog(ILogDto entity)
        {
            this._collection.InsertOne((TCollection)entity);
        }
    }
}
