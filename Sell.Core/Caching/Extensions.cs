using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Sell.Core.Caching
{
    public static class Extensions
    {
        public static  T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> GetFromDb)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = GetFromDb();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
            return result;
        }

        public static async Task<T> GetAsych<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<Task<T>> GetFromDb)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result =await GetFromDb();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
            return result;
        }
    }
}
