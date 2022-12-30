using CoreLayer.Utilities.Cache.Abstract;
using CoreLayer.Utilities.Ioc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Cache.Concreate
{
    public class MemoryCacheWorkFlow : ICacheWorkFlow
    {
        private string keyListName = "Cache_Key_List";
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheWorkFlow()
        {
            this._memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add<T>(string key, T data, double cacheTime)
        {
            _memoryCache.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
            KeyListAdd(key);
        }

        public void Add<T>(string key, T data, DateTime cacheDateTime)
        {
            _memoryCache.Set(key, data, cacheDateTime);
            KeyListAdd(key);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public object Get(string key, Type type)
        {
            var data = _memoryCache.Get(key);
            if (data != null)
            {
                string strData = JsonConvert.SerializeObject(data);
                return JsonConvert.DeserializeObject(strData, type);
            }
            else
                return null;
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            List<string> keys = new List<string>();
            if (IsAdd(keyListName))
            {
                keys = _memoryCache.Get<List<string>>(keyListName);
            }
            List<string> removeKeyList = new List<string>();
            if (pattern.EndsWith("*"))
            {
                string key = pattern.TrimEnd('*');
                removeKeyList = keys.Where(x => x.Contains(key)).ToList();
            }
            else
                removeKeyList = keys.Where(x=> x == pattern).ToList();
            
            foreach (var item in removeKeyList)
            {
                _memoryCache.Remove(item);
                keys.Remove(item);
            }
            if (keys.Count() > 0)
                _memoryCache.Set(keyListName, keys);
            else
                _memoryCache.Remove(keyListName);
        }
        public void Clear()
        {
            List<string> keys = new List<string>();
            if (IsAdd(keyListName))
            {
                keys = _memoryCache.Get<List<string>>(keyListName);
            }
            foreach (var item in keys)
            {
                _memoryCache.Remove(item);
            }
            _memoryCache.Remove(keyListName);
        }
        private void KeyListAdd(string key)
        {
            List<string> keys = new List<string>();
            if (IsAdd(keyListName))
            {
                keys = _memoryCache.Get<List<string>>(keyListName);
            }
            keys.Add(key);
            _memoryCache.Set(keyListName, keys);
        }
    }
}
