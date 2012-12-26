using System;
using System.Runtime.CompilerServices;
using System.Web;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace HWIX.Cache {
    public sealed class MemCache {
        private static MemCache _current;
        private static readonly object _memCacheSync = new object();

        public static MemCache Current {
            get {
                if (_current == null) {
                    lock (_memCacheSync) {
                        if (_current == null) {
                            _current = new MemCache();
                        }
                    }
                }
                return _current;
            }
        }

        public static DateTime MidnightExpiration {
            get { return DateTime.Today.AddDays(1); }
        }

        private const string _metaDataSection = "enyim.com/metadata";
        private readonly MemcachedClient _metaDataCache;
        private const string _googleSection = "enyim.com/google";
        private readonly MemcachedClient _googleCache;
        private const string _analyticsSection = "enyim.com/analytics";
        private readonly MemcachedClient _analyticsCache;
        private const string _dataWarehouseSection = "enyim.com/datawarehouse";
        private readonly MemcachedClient _dataWarehouseCache;

        private MemCache() {
            _metaDataCache = new MemcachedClient(_metaDataSection);
            _googleCache = new MemcachedClient(_googleSection);
            _analyticsCache = new MemcachedClient(_analyticsSection);
            _dataWarehouseCache = new MemcachedClient(_dataWarehouseSection);
        }

        public T Get<T>(CacheType cache, string key, [CallerMemberName] string callerName = null) {
            key = EncodeKey(key, callerName);
            return GetClient(cache).Get<T>(key);
        }

        public T Get<T>(CacheType cache, string key, Func<T> getter, [CallerMemberName] string callerName = null) {
            var result = Get<T>(cache, key, callerName);
            if (Equals(result, default(T))) {
                result = getter();
                Store(cache, StoreMode.Set, key, result, callerName);
            }
            return result;
        }

        public T Get<T>(CacheType cache, string key, TimeSpan validFor, Func<T> getter, [CallerMemberName] string callerName = null) {
            var result = Get<T>(cache, key, callerName);
            if (Equals(result, default(T))) {
                result = getter();
                Store(cache, StoreMode.Set, key, result, validFor, callerName);
            }
            return result;
        }

        public T Get<T>(CacheType cache, string key, DateTime expiresAt, Func<T> getter, [CallerMemberName] string callerName = null) {
            var result = Get<T>(cache, key, callerName);
            if (Equals(result, default(T))) {
                result = getter();
                Store(cache, StoreMode.Set, key, result, expiresAt, callerName);
            }
            return result;
        }

        public bool Store(CacheType cache, StoreMode mode, string key, object value, [CallerMemberName] string callerName = null) {
            key = EncodeKey(key, callerName);
            return GetClient(cache).Store(mode, key, value);
        }

        public bool Store(CacheType cache, StoreMode mode, string key, object value, TimeSpan validFor, [CallerMemberName] string callerName = null) {
            key = EncodeKey(key, callerName);
            return GetClient(cache).Store(mode, key, value, validFor);
        }

        public bool Store(CacheType cache, StoreMode mode, string key, object value, DateTime expiresAt, [CallerMemberName] string callerName = null) {
            key = EncodeKey(key, callerName);
            return GetClient(cache).Store(mode, key, value, expiresAt);
        }

        private string EncodeKey(string key, string callerName) {
            if (!string.IsNullOrEmpty(callerName)) {
                key = callerName + key;
            }
            return HttpUtility.UrlEncode(key);
        }

        private MemcachedClient GetClient(CacheType cache) {
            var client = default(MemcachedClient);
            switch (cache) {
                case CacheType.MetaData:
                    client = _metaDataCache;
                    break;
                case CacheType.Google:
                    client = _googleCache;
                    break;
                case CacheType.Analytics:
                    client = _analyticsCache;
                    break;
                case CacheType.DataWarehouse:
                    client = _dataWarehouseCache;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return client;
        }
    }

    public enum CacheType {
        MetaData,
        Analytics,
        DataWarehouse,
        Google
    }
}