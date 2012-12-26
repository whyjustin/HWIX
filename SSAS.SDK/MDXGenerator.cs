using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;

namespace SSAS.SDK {
    public class MDXGenerator {
        private readonly string _cube;

        public MDXGenerator(string cube) {
            _cube = cube;
        }

        /// <summary>
        /// Returns all member names
        /// </summary>
        /// <param name="qualifiedMember">Member to query i.e. [Geography].[County and Submarket]</param>
        /// <param name="member">Member to query within the hierarchy defined by qualifiedMember i.e. [County]</param>
        public string QueryMember(string qualifiedMember, string member) {
            return string.Format("with " +
                                 "member [path] as generate(hierarchize(filter(ascendants({0}.currentmember),{0}.currentmember.level.ordinal>1)),coalesceempty({0}.currentmember.parent.member_caption,{0}.currentmember.parent.member_name) + \"->\") + coalesceempty({0}.currentmember.member_caption,{0}.currentmember.member_name) " +
                                 "member [props] as {0}.CurrentMember.Name " +
                                 "select {{[props],[path]}} on columns, {{HIERARCHIZE(AddCalculatedMembers({{{0}.{1}.AllMembers}}))}} " +
                                 "dimension properties member_unique_name on rows from {2}", qualifiedMember, member, _cube);
        }

        /// <summary>
        /// Returns all member names that match a specific query
        /// </summary>
        /// <param name="qualifiedMember">Member to query i.e. [Geography].[County and Submarket]</param>
        public string QueryMember(QueryType type, string qualifiedMember, string query) {
            return string.Format("with " +
                                 "member [path] as generate(hierarchize(filter(ascendants({0}.currentmember),{0}.currentmember.level.ordinal>1)),coalesceempty({0}.currentmember.parent.member_caption,{0}.currentmember.parent.member_name) + \"->\") + coalesceempty({0}.currentmember.member_caption,{0}.currentmember.member_name) " +
                                 "member [props] as {0}.CurrentMember.Name " +
                                 "select {{[props],[path]}} on columns,  {{Subset(Order(AddCalculatedMembers({{Filter({0}.AllMembers, {1} )}}),[props],BAsc),0,50)}} " +
                                 "dimension properties member_unique_name on rows from {2}"
                                 , qualifiedMember, FilterFromEnum(type, qualifiedMember, query), _cube);
        }

        /// <summary>
        /// Returns member values by week
        /// </summary>
        /// <param name="uniqueName">UniqueName of member i.e. [Geography].[County and Submarket].[County].&[176]</param>
        /// <param name="dimension">Dimension i.e. [Time].[Week]</param>
        /// <param name="measure">The measure to query. i.e. [Measures].DefaultMember Optional: Default returns the default memeber</param>
        public string MemberByDimension(string uniqueName, string dimension, string measure = "[Measures].DefaultMember") {
            return string.Format("SELECT " +
                                 "{{{0}}} on 0, " +
                                 "NON EMPTY HIERARCHIZE( Distinct({{Hierarchize({{{1}.[All].Children}})}})) on 1 " +
                                 "From " +
                                 "(select {{{2}}} dimension properties member_unique_name on 0 " +
                                 "From {3})", measure, dimension, uniqueName, _cube);
        }

        private static string FilterFromEnum(QueryType type, string qualifiedMember, string query) {
            switch (type) {
                case QueryType.StartsWith:
                    return string.Format("left({0}.CurrentMember.Name,{1})=\"{2}\"", qualifiedMember, query.Length, query);
                case QueryType.Contains:
                    return string.Format("InStr(1, {0}.CurrentMember.Name, \"{1}\") <> 0", qualifiedMember, query);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum QueryType {
        StartsWith,
        Contains
    }
}
