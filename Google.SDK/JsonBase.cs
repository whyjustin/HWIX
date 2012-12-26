using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Google.SDK {
    public abstract class JsonBase {
        protected readonly string _apiKey;
        protected JsonBase(string apiKey) {
            _apiKey = apiKey;
        }

        public static string Request(string url, WebHeaderCollection headers = null) {
            var request = WebRequest.Create(url);
            if (headers != null) {
                request.Headers = headers;
            }
            using (var response = request.GetResponse()) {
                using (var stream = response.GetResponseStream()) {
                    using (var streamReader = new StreamReader(stream)) {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        public static T Request<T>(string url, WebHeaderCollection headers = null) {
            var request = WebRequest.Create(url);
            if (headers != null) {
                request.Headers = headers;
            }
            using (var response = request.GetResponse()) {
                using (var stream = response.GetResponseStream()) {
                    return Deserialize<T>(stream);
                }
            }
        }

        public static T Deserialize<T>(string json) {
            var byteArray = Encoding.ASCII.GetBytes(json);
            using (var stream = new MemoryStream(byteArray)) {
                return Deserialize<T>(stream);
            }
        }

        public static T Deserialize<T>(Stream stream) {
            var deserializer = new DataContractJsonSerializer(typeof(T));
            return (T)deserializer.ReadObject(stream);
        }
    }
}
