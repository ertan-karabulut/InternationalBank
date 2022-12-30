using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Cache.Abstract
{
    public interface ICacheWorkFlow
    {
        T Get<T>(string key);
        object Get(string key);
        object Get(string key, Type type);
        void Add<T>(string key, T data, double cacheTime);
        void Add<T>(string key, T data, DateTime cacheDateTime);
        bool IsAdd(string key);
        void Remove(string key);
        void Clear();
        void RemoveByPattern(string pattern);
    }
}
