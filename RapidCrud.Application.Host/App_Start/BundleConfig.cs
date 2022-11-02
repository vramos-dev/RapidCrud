using System.Web;
using System.Web.Optimization;

namespace RapidCrud.Application.Host
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery.toast.css"));

            bundles.Add(new StyleBundle("~/Site/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/jquery.toast.js",
                        "~/Scripts/Shared/Extension.js",
                        "~/Scripts/Shared/Util.js"));

            bundles.Add(new ScriptBundle("~/bundles/agenda").Include(
                       "~/Scripts/Agenda/agenda.js"));
        }
    }
}
