using System;
using System.Runtime.CompilerServices;
using Enyim.Caching.Memcached;

namespace HWIX.Cache {
    public class ScopedCache : IDisposable {
        public void Dispose() {}

        private CacheType _cacheType;
        public ScopedCache(CacheType cacheType) {
            _cacheType = cacheType;
        }

        public T Get<T>(string key, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Get<T>(_cacheType, key, callerName);
        }

        public T Get<T>(string key, Func<T> getter, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Get(_cacheType, key, getter, callerName);
        }

        public T Get<T>(string key, TimeSpan validFor, Func<T> getter, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Get(_cacheType, key, validFor, getter, callerName);
        }

        public T Get<T>(string key, DateTime expiresAt, Func<T> getter, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Get(_cacheType, key, expiresAt, getter, callerName);
        }

        public bool Store(StoreMode mode, string key, object value, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Store(_cacheType, mode, key, value, callerName);
        }

        public bool Store(StoreMode mode, string key, object value, TimeSpan validFor, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Store(_cacheType, mode, key, value, validFor, callerName);
        }

        public bool Store(StoreMode mode, string key, object value, DateTime expiresAt, [CallerMemberName] string callerName = null) {
            return MemCache.Current.Store(_cacheType, mode, key, value, expiresAt, callerName);
        }
    }
}