using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enyim.Caching.Memcached;
using Google.SDK;
using HWIX.Cache;
using HWIX.Models.Analytics;
using HWIX.Models.Google;
using HWIX.Models.Meta;
using HWIX.Models.Search;
using SSAS.SDK;

namespace HWIX.Controllers {
    public class SearchController : Controller {
        public JsonResult QueryGeography(string query) {
            var members = QueryMembers(query);
            var results = members.SelectMany(x => x.Value).Select(x => x.Name).OrderBy(x => x);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
            
        [HttpGet]
        public ActionResult Search(string query) {
            var search = GetSearch(query);
            ViewBag.Query = search.Name;
            return View(search);
        }
        
        [HttpGet]
        public ActionResult Map(string query) {
            var search = GetSearch(query);
            ViewBag.Query = search.Name;
            return View(search);
        }

        [HttpGet]
        public JsonResult SearchJSON(string uniqueName, string measure, string dimension) {
            var measureType = (MeasureType)Enum.Parse(typeof(MeasureType), measure);
            var dimensionType = (DimensionType) Enum.Parse(typeof (DimensionType), dimension);
            var member = GetMemberUnique(uniqueName);
            var search = new Search(member, measureType, dimensionType);
            return Json(search, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Suggestions(string query) {
            var members = QueryMembers(query);
            return View(members);
        }

        public Dictionary<Type, IEnumerable<IAnalyticsMember>> QueryMembers(string query) {
            var countyCache = new MetaCache<CountyMember>();
            var countyResults = countyCache.GetMember(QueryType.StartsWith, query).Cast<IAnalyticsMember>();
            var builderCache = new MetaCache<BuilderMember>();
            var builderResults = builderCache.GetMember(QueryType.StartsWith, query).Cast<IAnalyticsMember>();
            return new Dictionary<Type, IEnumerable<IAnalyticsMember>> {
                { typeof(CountyMember), countyResults},
                { typeof(BuilderMember), builderResults}
            };
        }

        private Search GetSearch(string query) {
            var members = QueryMembers(query);
            var memberName = members.OrderByDescending(x => x.Key == typeof (CountyMember)).SelectMany(x => x.Value).Select(x => x.UniqueName).First();
            var queryMember = GetMemberUnique(memberName);
            return new Search(queryMember, MeasureType.Closing, DimensionType.Week, members.SelectMany(x => x.Value).Count() > 1, query);
        }

        private IAnalyticsMember GetMemberByName(string query) {
            var queryMember = default(IAnalyticsMember);
            var countyCache = new MetaCache<CountyMember>();
            var county = countyCache.GetMember(query);
            if (county != null) {
                queryMember = county;
            }
            var builderCache = new MetaCache<BuilderMember>();
            var builder = builderCache.GetMember(query);
            if (builder != null) {
                queryMember = builder;
            }
            return queryMember;
        }

        private IAnalyticsMember GetMemberUnique(string uniqueName) {
            var queryMember = default(IAnalyticsMember);
            var countyCache = new MetaCache<CountyMember>();
            var county = countyCache.GetMemberUnique(uniqueName);
            if (county != null) {
                queryMember = county;
            }
            var builderCache = new MetaCache<BuilderMember>();
            var builder = builderCache.GetMemberUnique(uniqueName);
            if (builder != null) {
                queryMember = builder;
            }
            return queryMember;
        }
    }
}
