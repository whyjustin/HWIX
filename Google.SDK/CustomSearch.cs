using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.SDK {
    public class CustomSearch : JsonBase {
        private readonly string _cx;

        private const string _query = "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}";

        public CustomSearch(string apiKey, string cx)
            : base(apiKey) {
            _cx = cx;
        }

        public GoogleSearchResponse Query(string query) {
            var url = string.Format(_query, _apiKey, _cx, query);
            return Request<GoogleSearchResponse>(url);
        }
    }
}
