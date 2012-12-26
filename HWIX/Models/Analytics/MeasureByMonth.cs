using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AnalysisServices.AdomdClient;

namespace HWIX.Models.Analytics {
    [Serializable]
    public class MeasureByMonth : IMeasureByTimeDimension {
        public DateTime Month { get; set; }
        public double? Values { get; set; }

        public DateTime Time {
            get { return Month; }
        }

        public MeasureByMonth(AdomdDataReader reader, string measure) {
            var member = reader["[Time].[Month].[Month].[MEMBER_CAPTION]"] as string;
            Month = DateTime.ParseExact(member, "MMMM yyyy", CultureInfo.InvariantCulture);
            Values = reader.IsDBNull(reader.GetOrdinal(measure)) ? (double?)null : double.Parse(reader[measure].ToString());
        }
    }
}