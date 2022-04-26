using System.Web;
using System.Web.Optimization;

namespace MultiRowLOB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/explorer").Include(
                "~/Scripts/MultiLevelMenu.js", "~/Scripts/Layout.js"));
            bundles.Add(new StyleBundle("~/Content/css/explorer").Include(
                "~/Content/css/explorer.css", "~/Content/css/site.css"));
            bundles.Add(new StyleBundle("~/Content/appcss").Include(
                "~/Content/css/app.css"));
            bundles.Add(new StyleBundle("~/BootStrapCss").Include(
                "~/Content/css/bootstrap/css/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/BootStrapJS").Include(
                "~/Scripts/BootStrap/bootstrap.js"));
        }
    }
}
