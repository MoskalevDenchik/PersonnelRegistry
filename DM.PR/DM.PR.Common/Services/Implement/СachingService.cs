using System.Collections.Generic;
using System.Runtime.Caching;
using DM.PR.Common.Helpers;
using DM.PR.Common.Logger;
using System;

namespace DM.PR.Common.Services.Implement
{
    internal class СachingService : IСachingService
    {
        private IRecordLog _log;

        private MemoryCache _cache;

        private List<string> _keyList;

        public СachingService(IRecordLog log)
        {
            Inspector.ThrowExceptionIfNull(log);
            _log = log;
            _cache = new MemoryCache("Aplication");
            _keyList = new List<string>();

        }

        public T Get<T>(string key) where T : class
        {
            return _cache[key] as T;
        }

        public bool Add<T>(string key, T value, int seconds)
        {
            _keyList.Add(key);
            return _cache.Add(key, value, DateTime.Now.AddHours(seconds));
        }

        public void Update<T>(string key, T value, int seconds)
        {
            _keyList.Add(key);
            _cache.Set(key, value, DateTime.Now.AddSeconds(seconds));
        }

        public void Delete(string key)
        {
            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
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

        public T AddOrGetExisting<T>(string key, T value, int seconds) where T : class
        {
            return _cache.AddOrGetExisting(key, value, DateTime.Now.AddSeconds(seconds)) as T;
        }
    }
}
