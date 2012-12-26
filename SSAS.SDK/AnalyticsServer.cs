using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using SSAS.SDK.Extensions;

namespace SSAS.SDK {
    public class AnalyticsServer : IDisposable {
        private readonly AdomdConnection _connection;

        public AnalyticsServer(string connectionString) {
            _connection = new AdomdConnection(connectionString);
        }

        public IEnumerable<Member> GetMembers(string cubeName, string dimensionName, string hierachyName, string levelName) {
            if (_connection.State != ConnectionState.Open) {
                _connection.Open();
            }
            var level = _connection.Cubes[cubeName.StripBrackets()].Dimensions[dimensionName.StripBrackets()].Hierarchies[hierachyName.StripBrackets()].Levels[levelName.StripBrackets()];
            return level.GetMembers().Cast<Member>();
        }

        public List<T> PopulateList<T>(Tuple<string, Func<AdomdDataReader, T>> populateMeta) {
            return PopulateList(populateMeta.Item1, populateMeta.Item2);
        }

        public List<T> PopulateList<T>(string mdx, Func<AdomdDataReader, T> populateFunction) {
            var returnList = new List<T>();
            if (_connection.State != ConnectionState.Open) {
                _connection.Open();
            }
            using (var command = new AdomdCommand(mdx, _connection)) {
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        returnList.Add(populateFunction(reader));
                    }
                }
            }
            return returnList;
        }

        public void Dispose() {
            _connection.Dispose();
        }
    }
}
