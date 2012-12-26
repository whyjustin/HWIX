using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AnalysisServices.AdomdClient;
using SSAS.SDK;

namespace HWIX.Models.Meta {
    [Serializable]
    public class BuilderMember : AnalyticsMember {
        public BuilderMember(Member member)
            : base(member) {
        }
    }
}