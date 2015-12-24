using System.Web.Optimization;

namespace Cake.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js/jquery").Include(
                        "~/Content/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Content/js/jqueryval").Include(
                        "~/Content/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Content/js/modernizr").Include(
                        "~/Content/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Content/js/bootstrap").Include(
                      "~/Content/js/bootstrap.js",
                      "~/Content/js/respond.js"));

            bundles.Add(new ScriptBundle("~/Content/js/prism").Include(
                      "~/Content/js/prism.js"));

            bundles.Add(new ScriptBundle("~/Content/js/highlight").Include(
                      "~/Content/js/highlight.pack.js"));

            bundles.Add(new ScriptBundle("~/Content/js/snowstorm").Include(
                      "~/Content/js/snowstorm-min.js"));

            bundles.Add(new StyleBundle("~/Content/css/files").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/cake-theme.css",
                      "~/Content/css/global.css"));

            bundles.Add(new StyleBundle("~/Content/css/prism").Include(
                "~/Content/css/prism.css"));

            bundles.Add(new StyleBundle("~/Content/css/highlight").Include(
                "~/Content/css/obsidian.css"));
        }
    }
}
