using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HWIX.Cache;
using Microsoft.AnalysisServices.AdomdClient;
using SSAS.SDK;

namespace HWIX.Models.Meta {
    [Serializable]
    public class CountyMember : AnalyticsMember {
        public CountyMember(Member member)
            : base(member) {
        }
    }
}