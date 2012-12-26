using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Enyim.Caching.Memcached;
using HWIX.Models.Meta;
using SSAS.SDK;
using HWIX.Extensions;

namespace HWIX.Cache {
    public class MetaCache<T> where T : class, IAnalyticsMember {
        private const string _queriedMemberKey = "ALLQUERIEDMEMBERS";
        private readonly string _typeName;
        private string GetQueriedMemberKey() {
            return _queriedMemberKey + _typeName;
        }

        private static readonly object _queriedMemberSync = new object();
        private static List<T> _allMembers;
        private List<T> GetAllQueriedMembers() {
            if (_allMembers != null) {
                return _allMembers;
            }
            using (var scopedCache = new ScopedCache(CacheType.MetaData)) {
                return scopedCache.Get<List<T>>(GetQueriedMemberKey(), string.Empty);
            }
        }

        public MetaCache() {
            _typeName = typeof (T).FullName;

            var queriedMembers = GetAllQueriedMembers();
            if (queriedMembers == null) {
                lock (_queriedMemberSync) {
                    queriedMembers = GetAllQueriedMembers();
                    if (queriedMembers == null) {
                        using (var server = new AnalyticsServer(Config.AnalyticsConnectionString)) {
                            var memberMeta = MemberMeta.GetMemberMeta<T>();
                            var results = server.GetMembers(Config.HomeSalesCubeName, memberMeta.Dimension, memberMeta.Hierarchy, memberMeta.Level);
                            _allMembers = results.Select(memberMeta.BuildFunction).Cast<T>().ToList();
                            using (var scopedCache = new ScopedCache(CacheType.MetaData)) {
                                scopedCache.Store(StoreMode.Set, GetQueriedMemberKey(), _allMembers, MemCache.MidnightExpiration, string.Empty);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<T> GetMember(QueryType type, string query) {
            var members = GetAllQueriedMembers();
            switch (type) {
                case QueryType.StartsWith:
                    return members.Where(x => x.Name.StartsWith(query, true));
                case QueryType.Contains:
                    return members.Where(x => x.Name.Contains(query, true));
                default:
                    throw new NotImplementedException();
            }
        }

        public T GetMember(string name) {
            var members = GetAllQueriedMembers();
            return members.FirstOrDefault(x => x.Name == name);
        }

        public T GetMemberUnique(string uniqueName) {
            var members = GetAllQueriedMembers();
            return members.FirstOrDefault(x => x.UniqueName == uniqueName);
        }
    }
}