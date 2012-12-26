using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Google.SDK;

namespace HWIX.Cache {
    public class GoogleCache {
        public IEnumerable<SearchItem> Search(string query) {
            var getter = new Func<List<SearchItem>>(() => {
                var googleSearch = new CustomSearch(Config.GoogleAPIKey, Config.GoogleCX);
                return googleSearch.Query(query).Items;
            });
            using (var googleCache = new ScopedCache(CacheType.Google)) {
                return googleCache.Get(query, MemCache.MidnightExpiration, getter);
            }
        }
    }
}