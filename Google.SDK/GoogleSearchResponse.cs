using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Google.SDK {
    [DataContract]
    [Serializable]
    public class GoogleSearchResponse {
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "url")]
        public Url Url { get; set; }

        [DataMember(Name = "queries")]
        public Queries Queries { get; set; }

        [DataMember(Name = "context")]
        public Context Context { get; set; }

        [DataMember(Name = "items")]
        public List<SearchItem> Items { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Url {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "template")]
        public string Template { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Queries {
        [DataMember(Name = "nextPage")]
        public List<Page> NextPage { get; set; }

        [DataMember(Name = "request")]
        public List<Page> Request { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Page {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "totalResults")]
        public int Request { get; set; }

        [DataMember(Name = "searchTerms")]
        public string SearchTerms { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "startIndex")]
        public int StartIndex { get; set; }

        [DataMember(Name = "inputEncoding")]
        public string InputEncoding { get; set; }

        [DataMember(Name = "outputEncoding")]
        public string OutputEncoding { get; set; }

        [DataMember(Name = "safe")]
        public string Safe { get; set; }

        [DataMember(Name = "cx")]
        public string CX { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Context {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "facets")]
        public List<List<Facet>> Facets { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Facet {
        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "anchor")]
        public string Anchor { get; set; }
    }

    [DataContract]
    [Serializable]
    public class SearchItem {
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "htmlTitle")]
        public string HtmlTitle { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "displayLink")]
        public string DisplayLink { get; set; }

        [DataMember(Name = "snippet")]
        public string Snippet { get; set; }

        [DataMember(Name = "htmlSnippet")]
        public string HtmlSnippet { get; set; }

        [DataMember(Name = "cacheId")]
        public string CacheId { get; set; }

        [DataMember(Name = "pagemap")]
        public Pagemap Pagemap { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Pagemap {
        [DataMember(Name = "cse_thumbnail")]
        public List<Thumbnail> Thumbnails { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Thumbnail {
        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }

        [DataMember(Name = "src")]
        public string Source { get; set; }
    }
}