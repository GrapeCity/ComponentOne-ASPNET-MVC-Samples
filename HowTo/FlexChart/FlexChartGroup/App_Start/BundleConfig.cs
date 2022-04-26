using System.Web;
using System.Web.Optimization;

namespace FlexChartGroup
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/script/External/Jquery").Include("~/Content/External/jquery.js"));
            bundles.Add(new StyleBundle("~/css/External/BootStrap").Include("~/Content/External/bootstrap/css/bootstrap.css"));

            bundles.Add(new StyleBundle("~/css/Styles").Include("~/Content/Styles/app.css"));
        }
    }
}