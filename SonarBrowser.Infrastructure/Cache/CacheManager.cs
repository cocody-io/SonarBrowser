using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using ServiceStack.Caching;
using ServiceStack.Caching.Memcached;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarBrowser.Infrastructure.Cache
{
    public class CacheManager : ICacheManager
    {
        public ICacheClient CacheClient{ get; private set; }
        public TimeSpan CacheDuration { get; set; }

        public CacheManager(ICacheClient cacheClient)
        {
            this.CacheClient = cacheClient;
           // CacheDuration = TimeSpan.FromHours(cacheDuration);
        }

        public virtual T Resolve<T>(string cacheKey, Func<T> createCacheFn)
                where T : class
        {
            var result = this.CacheClient.Get<T>(cacheKey);

            if (result != null)
            {
                return result;
            }

            var cacheValue = createCacheFn();

            this.CacheClient.Set(cacheKey, cacheValue, CacheDuration);

            return cacheValue;
        }
    }
}
