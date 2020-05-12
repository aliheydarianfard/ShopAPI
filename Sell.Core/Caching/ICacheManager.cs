using System;

namespace Sell.Core.Caching
{

    public interface ICacheManager: IDisposable
    {
       
        T Get<T>(string key);

    
       
        void Set(string key, object data, int cacheTime);

       
        bool IsSet(string key);

     
        void Remove(string key);

      

       
        void Clear();
    }
}
