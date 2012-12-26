using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AnalysisServices.AdomdClient;

namespace HWIX.Models.Analytics {
    [Serializable]
    public class MeasureByWeek : IMeasureByTimeDimension {
        private readonly static int _weekPrefix = "Week of ".Length;

        public DateTime Week { get; set; }
        public double? Values { get; set; }

        public DateTime Time {
            get { return Week; }
        }

        public MeasureByWeek(AdomdDataReader reader, string measure) {
            var member = reader["[Time].[Week].[Week].[MEMBER_CAPTION]"] as string;
            var weekStr = member.Substring(_weekPrefix);
            Week = DateTime.Parse(weekStr);
            Values = reader.IsDBNull(reader.GetOrdinal(measure)) ? (double?)null : double.Parse(reader[measure].ToString());
        }
    }
}