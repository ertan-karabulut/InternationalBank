using CoreLayer.Utilities.Cache.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Cache.Concreate
{
    public class RedisCacheWorkFlow : ICacheWorkFlow
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private readonly string _host = "localhost";
        private readonly int _port = 6379;
        private readonly int _db = 0;
        private readonly string _applicationName = "";
        public RedisCacheWorkFlow(string host, int port, int db = 0, string applicationName = "")
        {
            _host = host;
            _port = port;
            _db = db;
            _connectionMultiplexer = ConnectionMultiplexer.Connect($"{host}:{port}");
            _database = _connectionMultiplexer.GetDatabase(db);
            _applicationName = applicationName;
        }
        public RedisCacheWorkFlow()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
            _database = _connectionMultiplexer.GetDatabase(_db);
        }
        public void Add<T>(string key, T data, double cacheTime)
        {
            TimeSpan timeSpan = DateTime.Now.AddMinutes(cacheTime) - DateTime.Now;
            _database.StringSet($"{this._applicationName}_{key}", JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), timeSpan);
        }

        public void Add<T>(string key, T data, DateTime cacheDateTime)
        {
            TimeSpan timeSpan = cacheDateTime - DateTime.Now;
            _database.StringSet($"{this._applicationName}_{key}", JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), timeSpan);
        }
        public T Get<T>(string key)
        {
            if (_database.KeyExists($"{this._applicationName}_{key}"))
                return JsonConvert.DeserializeObject<T>(_database.StringGet($"{this._applicationName}_{key}"));
            else
                return default(T);
        }
        public object Get(string key, Type type)
        {
            if (_database.KeyExists($"{this._applicationName}_{key}"))
                return JsonConvert.DeserializeObject(_database.StringGet($"{this._applicationName}_{key}"), type);
            else
                return null;
        }
        public object Get(string key)
        {
            if (_database.KeyExists($"{this._applicationName}_{key}"))
                return JsonConvert.DeserializeObject<object>(_database.StringGet($"{this._applicationName}_{key}"));
            else
                return null;
        }

        public bool IsAdd(string key)
        {
            return _database.KeyExists($"{this._applicationName}_{key}");
        }

        public void Remove(string key)
        {
            _database.KeyDelete($"{this._applicationName}_{key}");
        }

        public void RemoveByPattern(string pattern)
        {
            var server = _connectionMultiplexer.GetServer(_host, _port);
            foreach (var key in server.Keys(pattern: $"{this._applicationName}_{pattern}"))
            {
                _database.KeyDelete(key);
            }
        }

        public void Clear()
        {
            var server = _connectionMultiplexer.GetServer(_host, _port);
            foreach (var key in server.Keys(pattern: $"{this._applicationName}_*"))
            {
                _database.KeyDelete(key);
            }
        }
    }
}
