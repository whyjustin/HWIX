using System;
using Microsoft.AnalysisServices.AdomdClient;
using SSAS.SDK;

namespace HWIX.Models.Meta {
    public class MemberMeta {
        public string Dimension { get; private set; }
        public string Hierarchy { get; private set; }
        public string Level { get; private set; }
        public Func<Member, IAnalyticsMember> BuildFunction { get; private set; }

        public MemberMeta(string dimension, string hierarchy, string level, Func<Member, IAnalyticsMember> buildFunction) {
            Dimension = dimension;
            Hierarchy = hierarchy;
            Level = level;
            BuildFunction = buildFunction;
        }

        public static MemberMeta GetMemberMeta<T>() where T : IAnalyticsMember {
            switch (typeof(T).Name) {
                case "CountyMember":
                    return new MemberMeta("[Geography]", "[State and County]", "[County]", member => new CountyMember(member));
                case "BuilderMember":
                    return new MemberMeta("[Builder]", "[Builder]", "[Builder Name]", member => new BuilderMember(member));
                default:
                    throw new NotImplementedException();
            }
        }

        public static MemberType GetMemberType(IAnalyticsMember member) {
            if (member is CountyMember) {
                return MemberType.County;
            } else if (member is BuilderMember) {
                return MemberType.Builder;
            } else {
                throw new NotImplementedException();
            }
        }
    }
}