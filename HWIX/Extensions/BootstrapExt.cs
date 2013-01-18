using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HWIX.Extensions {
    public static class BootstrapExt {
        public static MvcHtmlString ActionIcon(this HtmlHelper html, string action, string controllerName, object routeValues, BootstrapButton btn, BootstrapIcon icon, bool isWhite = false) {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            var iBuilder = new TagBuilder("i");
            iBuilder.MergeAttribute("class", ClassFromIcon(icon, isWhite));
            string iHtml = iBuilder.ToString(TagRenderMode.Normal);

            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controllerName, routeValues));
            anchorBuilder.MergeAttribute("class", ClassFromButton(btn));
            anchorBuilder.InnerHtml = iHtml;
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        public static MvcHtmlString ActionIcon(this HtmlHelper html, string onClick, BootstrapButton btn, BootstrapIcon icon, bool isWhite = false) {
            var iBuilder = new TagBuilder("i");
            iBuilder.MergeAttribute("class", ClassFromIcon(icon, isWhite));
            string iHtml = iBuilder.ToString(TagRenderMode.Normal);

            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", "#");
            anchorBuilder.MergeAttribute("class", ClassFromButton(btn));
            anchorBuilder.MergeAttribute("onclick", onClick);
            anchorBuilder.InnerHtml = iHtml;
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        public static MvcHtmlString SubmitIcon(this HtmlHelper html, BootstrapButton btn, BootstrapIcon icon, bool isWhite = false) {
            var iBuilder = new TagBuilder("i");
            iBuilder.MergeAttribute("class", ClassFromIcon(icon, isWhite));
            string iHtml = iBuilder.ToString(TagRenderMode.Normal);

            var inputBuilder = new TagBuilder("button");
            inputBuilder.MergeAttribute("class", ClassFromButton(btn));
            inputBuilder.MergeAttribute("type", "submit");
            inputBuilder.InnerHtml = iHtml;
            string inputHtml = inputBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(inputHtml);
        }

        private static string ClassFromIcon(BootstrapIcon icon, bool isWhite = false) {
            var clss = string.Empty;
            switch (icon) {
             case BootstrapIcon.Search:
                    clss = "icon-search";
                    break;
                case BootstrapIcon.Left:
                    clss = "icon-arrow-left";
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (isWhite) {
                clss += " icon-white";
            }
            return clss;
        }

        private static string ClassFromButton(BootstrapButton button) {
            var btn = "btn";
            switch (button) {
                case BootstrapButton.Primary:
                    btn += " btn-primary";
                    break;
                case BootstrapButton.Info:
                    btn += " btn-info";
                    break;
                case BootstrapButton.Success:
                    btn += " btn-success";
                    break;
                case BootstrapButton.Warning:
                    btn += " btn-warning";
                    break;
                case BootstrapButton.Danger:
                    btn += " btn-danger";
                    break;
                case BootstrapButton.Inverse:
                    btn += " btn-inverse";
                    break;
                case BootstrapButton.Link:
                    btn += " btn-link";
                    break;
            }
            return btn;
        }
    }

    public enum BootstrapIcon {
        Search,
        Left
    }

    public enum BootstrapButton {
        Default,
        Primary,
        Info,
        Success,
        Warning,
        Danger,
        Inverse,
        Link
    }
}