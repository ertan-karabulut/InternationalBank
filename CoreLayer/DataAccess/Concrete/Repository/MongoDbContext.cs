using CoreLayer.DataAccess.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Concrete.Repository
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IDatabaseSttings databaseSttings)
        {
            var client = new MongoClient(databaseSttings.ConnectionString);
            this._database = client.GetDatabase(databaseSttings.DatabaseName);
        }

        public IMongoCollection<TCollection> GetCollection<TCollection>()
        {
            return this._database.GetCollection<TCollection>(typeof(TCollection).Name.Trim());
        }

        public IMongoDatabase GetDatabase()
        {
            return this._database;
        }
    }
}
