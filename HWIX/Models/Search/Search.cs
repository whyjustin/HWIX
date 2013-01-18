using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HWIX.Cache;
using HWIX.Models.Analytics;
using HWIX.Models.DataWarehouse;
using HWIX.Models.Google;
using HWIX.Models.Meta;
using SSAS.SDK;

namespace HWIX.Models.Search {
    public class Search {
        public bool HasOtherSuggestions { get; set; }
        public string OriginalQuery { get; set; }

        public string Name { get; set; }
        public string UniqueName { get; set; }
        public MemberType Type { get; set; }
        public IEnumerable<GoogleResult> GoogleResults { get; set; }
        public IEnumerable<IMeasureByTimeDimension> ValueByWeek { get; set; }
        public IEnumerable<TopProject> TopProjects { get; set; }
        public CountyExtent Extent { get; set; }

        public string Format { get; set; }

        public DateTime EndDate {
            get {
                var lastWeek = ValueByWeek.Where(x => (x.Values ?? 0) > 0).OrderByDescending(x => x.Time).Select(x => x.Time).First();
                return lastWeek.AddDays(-lastWeek.Day);
            }
        }

        public double CurrentValue {
            get { return ValueByWeek.Where(x => x.Time <= EndDate).OrderByDescending(x => x.Time).Select(x => x.Values ?? 0).First(); }
        }

        public double OneMonthDiff {
            get {
                return (ValueByWeek.Where(x => x.Time <= EndDate && x.Time > EndDate.AddMonths(-1)).Sum(x => x.Values) ?? 0)
                       - (ValueByWeek.Where(x => x.Time <= EndDate.AddMonths(-1) && x.Time > EndDate.AddMonths(-2)).Sum(x => x.Values) ?? 0);
            }
        }

        public double OneMonthPercent {
            get { return OneMonthDiff / CurrentValue; }
        }

        public double OneMonthLow {
            get { return ValueByWeek.Where(x => x.Time <= EndDate && x.Time > EndDate.AddMonths(-1)).Min(x => x.Values) ?? 0; }
        }

        public double OneMonthHigh {
            get { return ValueByWeek.Where(x => x.Time <= EndDate && x.Time > EndDate.AddMonths(-1)).Max(x => x.Values) ?? 0; }   
        }

        public double OneYearLow {
            get { return ValueByWeek.Where(x => x.Time <= EndDate && x.Time > EndDate.AddYears(-1)).Min(x => x.Values) ?? 0; }
        }

        public double OneYearHigh {
            get { return ValueByWeek.Where(x => x.Time <= EndDate && x.Time > EndDate.AddYears(-1)).Max(x => x.Values) ?? 0; }
        }

        public Search(IAnalyticsMember member, MeasureType measure, DimensionType dimension, bool hasOtherSuggestions = false, string originalQuery = null) {
            Name = member.Name;
            UniqueName = member.UniqueName;
            Type = MemberMeta.GetMemberType(member);
            Format = AnalyticsCache.FormatFromType(measure);

            HasOtherSuggestions = hasOtherSuggestions;
            OriginalQuery = originalQuery;

            try {
                var customSearch = new GoogleCache();
                GoogleResults = customSearch.Search(member.Name).Select(x => new GoogleResult(x));
            } catch(Exception ex) {
                // Not paying for google search so this can throw a Not Authorized
            }

            var analyticsCache = new AnalyticsCache();
            ValueByWeek = analyticsCache.GetMesaureByDimension(member, dimension, measure).Cast<IMeasureByTimeDimension>();

            var dataWarehouseCache = new DataWarehouseCache();
            TopProjects = dataWarehouseCache.GetTopProjects(member.Name, this.EndDate);

            switch (Type) {
                case MemberType.County:
                    Extent = dataWarehouseCache.GetCountyExtent(member.Name);
                    break;
            }
        }
    }
}