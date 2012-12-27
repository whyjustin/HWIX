using System.Web;
using System.Web.Optimization;

namespace HWIX {
    public class BundleConfig {
        public static string STYLE_SITE = "~/Content/css";
        public static string STYLE_BOOTSTRAP = "~/STYLE_BOOTSTRAP";
        public static string STYLE_JQUERY = "~/Content/themes/base/css";
        public static string STYLE_KENDOUI = "~/STYLE_KENDOUI";
        public static string STYLE_JQDATERANGESLIDER = "~/STYLE_STYLE_JQDATERANGESLIDER";

        public static string SCRIPT_HWIX = "~/SCRIPT_HWIX";
        public static string SCRIPT_BOOTSTRAP = "~/SCRIPT_BOOTSTRAP";
        public static string SCRIPT_JQUERY = "~/bundles/jquery";
        public static string SCRIPT_JQUERYUI = "~/bundles/jqueryui";
        public static string SCRIPT_JQUERYVAL = "~/bundles/jqueryval";
        public static string SCRIPT_MODENIZR = "~/bundles/modernizr";
        public static string SCRIPT_KENDOUI = "~/SCRIPT_KENDOUI";
        public static string SCRIPT_KNOCKOUT = "~/SCRIPT_KNOCKOUT";
        public static string SCRIPT_MAPDOTNET = "~/SCRIPT_MAPDOTNET";
        public static string SCRIPT_JQDATERANGESLIDER = "~/SCRIPT_JQDATERANGESLIDER";

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle(SCRIPT_JQUERY).Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle(SCRIPT_JQUERYUI).Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle(SCRIPT_JQUERYVAL).Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle(SCRIPT_MODENIZR).Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle(STYLE_SITE).Include("~/Content/site.css"));

            bundles.Add(new StyleBundle(STYLE_JQUERY).Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle(STYLE_BOOTSTRAP).Include("~/Content/bootstrap.css", "~/Content/bootstrap-responsive.css"));
            bundles.Add(new ScriptBundle(SCRIPT_BOOTSTRAP).Include("~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle(STYLE_KENDOUI).Include("~/Content/kendo*"));
            bundles.Add(new ScriptBundle(SCRIPT_KENDOUI).Include("~/Scripts/kendo*"));

            bundles.Add(new ScriptBundle(SCRIPT_KNOCKOUT).Include(
                        "~/Scripts/knockout-2.1.0.js",
                        "~/Scripts/knockout.mapping-latest.js"));

            bundles.Add(new ScriptBundle(SCRIPT_HWIX).Include("~/Scripts/hwix*"));

            bundles.Add(new ScriptBundle(SCRIPT_MAPDOTNET).Include(
                        "~/Scripts/isc.rim.js",
                        "~/Scripts/jquery.mousewheel.js"));

            bundles.Add(new StyleBundle(STYLE_JQDATERANGESLIDER).Include("~/Content/jQDateRangeSlider/classic.css"));
            bundles.Add(new ScriptBundle(SCRIPT_JQDATERANGESLIDER).Include("~/Scripts/jQDateRangeSlider-min.js"));
        }
    }
}