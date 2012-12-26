using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;

namespace SSAS.SDK {
    [Serializable]
    public abstract class AnalyticsMember : IAnalyticsMember {
        public string Name { get; set; }
        public string UniqueName { get; set; }

        protected AnalyticsMember(Member member) {
            Name = member.Name;
            UniqueName = member.UniqueName;
        }
    }

    public interface IAnalyticsMember {
        string Name { get; set; }
        string UniqueName { get; set; }
    }
}
