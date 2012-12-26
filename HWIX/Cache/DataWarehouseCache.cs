using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HWIX.Models.DataWarehouse;

namespace HWIX.Cache {
    public class DataWarehouseCache {
        public List<TopProject> GetTopProjects(string query, DateTime date) {
            var getter = (Func<List<TopProject>>)(() => {
                using (var warehouse = new DataWarehouseEntities()) {
                    return warehouse.GetTopProjects(query, date).ToList();
                }
            });
            using (var dataWarehouse = new ScopedCache(CacheType.DataWarehouse)) {
                return dataWarehouse.Get(query + date.ToShortDateString(), TimeSpan.FromMinutes(60), getter);
            }
        }

        public CountyExtent GetCountyExtent(string query) {
            var getter = (Func<CountyExtent>)(() => {
                using (var warehouse = new DataWarehouseEntities()) {
                    return warehouse.GetCountyExtents(query).FirstOrDefault();
                }
            });
            using (var dataWarehouse = new ScopedCache(CacheType.DataWarehouse)) {
                return dataWarehouse.Get(query, TimeSpan.FromMinutes(60), getter);
            }
        }
    }
}