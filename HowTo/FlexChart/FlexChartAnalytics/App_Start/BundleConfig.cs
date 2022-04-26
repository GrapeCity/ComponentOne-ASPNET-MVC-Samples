using System.Web;
using System.Web.Optimization;

namespace FlexChartAnalytics
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/External/Jquery").Include("~/Content/External/jquery.js"));
            bundles.Add(new StyleBundle("~/Content/External/BootStrap/css").Include("~/Content/External/bootstrap/css/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/Content/External/BootStrap").Include("~/Content/External/bootstrap/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/Styles/app").Include("~/Content/Styles/app.css"));
        }
    }
}