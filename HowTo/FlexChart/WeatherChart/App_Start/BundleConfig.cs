using System.Web;
using System.Web.Optimization;

namespace WeatherChart
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/External/external").Include("~/Content/External/*.js"));

            bundles.Add(new StyleBundle("~/Content/Styles/app").Include("~/Content/Styles/app.css"));
            bundles.Add(new ScriptBundle("~/Content/Scripts/scripts").Include("~/Content/Scripts/*.js"));
        }
    }
}