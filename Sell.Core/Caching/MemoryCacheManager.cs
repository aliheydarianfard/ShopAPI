using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sell.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache = null;

        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Clear()
        {
           
        }

        public void Dispose()
        {
           
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public bool IsSet(string key)
        {
            return _cache.TryGetValue(key, out object _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

     
        public void Set(string key, object data, int cacheTime)
        {
           if(data!=null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions();
                  cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTime);
                _cache.Set(key, data, cacheEntryOptions);
            }
        }
    }
}
