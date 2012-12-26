using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using HWIX.Models.Analytics;
using Microsoft.AnalysisServices.AdomdClient;
using SSAS.SDK;

namespace HWIX.Cache {
    public class AnalyticsCache {
        public IEnumerable<IMeasureByDimension> GetMesaureByDimension(IAnalyticsMember queriedMember, DimensionType dimension, MeasureType measure) {
            var measureStr = string.Empty;
            switch (measure) {
                case MeasureType.AveragePrice:
                    measureStr = "[Measures].[Closing Price Average]";
                    break;
                case MeasureType.Closing:
                    measureStr = "[Measures].[Closings]";
                    break;
                default:
                    throw new NotImplementedException();
            }
            var dimensionStr = string.Empty;
            var populateFunction = default(Func<AdomdDataReader, IMeasureByDimension>);
            switch (dimension) {
                case DimensionType.Week:
                    dimensionStr = "[Time].[Week]";
                    populateFunction = reader => new MeasureByWeek(reader, measureStr);
                    break;
                case DimensionType.Month:
                    dimensionStr = "[Time].[Month]";
                    populateFunction = reader => new MeasureByMonth(reader, measureStr);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return GetMesaureByDimension(queriedMember, dimensionStr, measureStr, populateFunction);
        }

        public static string FormatFromType(MeasureType measure) {
            switch (measure) {
                case MeasureType.AveragePrice:
                    return "$";
                case MeasureType.Closing:
                    return string.Empty;
                default:
                    throw new NotImplementedException();
            }
        }

        public IEnumerable<T> GetMesaureByDimension<T>(IAnalyticsMember queriedMember, string dimension, string measure, Func<AdomdDataReader, T> populateFunction) where T : IMeasureByDimension {
            var getter = new Func<List<T>>(() => {
                using (var server = new AnalyticsServer(Config.AnalyticsConnectionString)) {
                    var mdxGenerator = new MDXGenerator(Config.HomeSalesCubeName);
                    var mdx = mdxGenerator.MemberByDimension(queriedMember.UniqueName, dimension, measure);
                    return server.PopulateList(mdx, populateFunction);
                }
            });
            using (var analyticsCache = new ScopedCache(CacheType.Analytics)) {
                return analyticsCache.Get(queriedMember.UniqueName + measure, TimeSpan.FromMinutes(60), getter);
            }
        }
    }

    public enum DimensionType {
        Week,
        Month
    }

    public enum MeasureType {
        Closing,
        AveragePrice
    }
}