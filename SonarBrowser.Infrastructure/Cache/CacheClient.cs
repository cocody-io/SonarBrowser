using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using ServiceStack.Caching;
using System;
using System.Collections.Generic;

namespace SonarBrowser.Infrastructure.Cache
{
    public class CacheClient : ICacheClient
    {
        Microsoft.Practices.EnterpriseLibrary.Caching.ICacheManager _currentCacheManger = Microsoft.Practices.EnterpriseLibrary.Caching.CacheFactory.GetCacheManager();

        public bool Add<T>(string key, T value, TimeSpan expiresIn)
        {

            _currentCacheManger.Add(key, value, Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority.Normal, null, new AbsoluteTime(expiresIn));
            return true;
        }

        public bool Add<T>(string key, T value, DateTime expiresAt)
        {
            _currentCacheManger.Add(key, value, Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority.Normal, null, new AbsoluteTime(expiresAt));
            return false;
        }

        public bool Add<T>(string key, T value)
        {

            try
            {
                _currentCacheManger.Add(key, value);
                return true;
            }
            catch
            {
                return false;
            }


        }

        public long Decrement(string key, uint amount)
        {
            throw new NotImplementedException();
        }

        public void FlushAll()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            return (T)_currentCacheManger.GetData(key);
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public long Increment(string key, uint amount)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            try
            {
                _currentCacheManger.Remove(key);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            foreach (var item in keys)
            {
                Remove(item);
            }
        }

        public bool Replace<T>(string key, T value, TimeSpan expiresIn)
        {
            throw new NotImplementedException();
        }

        public bool Replace<T>(string key, T value, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public bool Replace<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            if (_currentCacheManger.Contains(key)) _currentCacheManger.Remove(key);
            return Add<T>(key, value, expiresIn);
        }

        public bool Set<T>(string key, T value, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public bool Set<T>(string key, T value)
        {
            if (_currentCacheManger.Contains(key)) _currentCacheManger.Remove(key);
            return Add<T>(key, value);

        }

        public void SetAll<T>(IDictionary<string, T> values)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
