using System;
using System.Collections.Generic;
using System.Configuration;
using HWIX.Models.Meta;
using Microsoft.AnalysisServices.AdomdClient;
using SSAS.SDK;

namespace HWIX.Cache {
    public static class Config {
        public static string HomeSalesCubeName = "[Home Sales]";
        public static string AnalyticsConnectionString {
            get { return ConfigurationManager.ConnectionStrings["HIPAnalytics"].ConnectionString; }
        }

        public static string GoogleAPIKey {
            get { return ConfigurationManager.AppSettings["googleAPIKey"]; }
        }

        public static string GoogleCX {
            get { return ConfigurationManager.AppSettings["googleCX"]; }
        }

        public static string BingMapsKey {
            get { return ConfigurationManager.AppSettings["bingMapsKey"]; }
        }

        public static string MDNEndPoint {
            get { return ConfigurationManager.AppSettings["mdnEndPoint"]; }
        }
    }
}