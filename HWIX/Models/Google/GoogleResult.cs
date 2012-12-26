using System.Linq;
using Google.SDK;
using HWIX.Extensions;

namespace HWIX.Models.Google {
    public class GoogleResult {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public string ThumbnailLink { get; set; }

        public GoogleResult(SearchItem searchItem) {
            Title = searchItem.HtmlTitle.TrimBreakLines();
            Description = searchItem.HtmlSnippet.TrimBreakLines();
            Link = searchItem.Link;
            ThumbnailLink = searchItem.Pagemap != null && searchItem.Pagemap.Thumbnails != null && searchItem.Pagemap.Thumbnails.Count > 0
                ? searchItem.Pagemap.Thumbnails.Select(x => x.Source).First() : null;
        }
    }
}