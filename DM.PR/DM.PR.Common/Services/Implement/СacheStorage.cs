using System.Collections.Generic;
using System.Runtime.Caching;
using System;

namespace DM.PR.Common.Services.Implement
{
    internal class СacheStorage : IСacheStorage, IDisposable
    {
        private MemoryCache _cache;

        public СacheStorage()
        {
            _cache = new MemoryCache("Aplication");
        }

        public T Get<T>(string key) where T : class
        {
            return _cache[key] as T;
        }

        public bool Add<T>(string key, T value, int seconds)
        {
            return _cache.Add(key, value, DateTime.Now.AddMinutes(seconds));
        }

        public void Delete(string key)
        {
            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }
        }

        public void DeleteWhoContains(string key)
        {
            foreach (KeyValuePair<string, object> item in _cache)
            {
                if (item.Key.Contains(key))
                {
                    Delete(item.Key);
                }

            }
        }
        private bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}
