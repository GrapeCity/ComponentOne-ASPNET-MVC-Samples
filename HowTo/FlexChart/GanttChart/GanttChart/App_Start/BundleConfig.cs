using System.Web.Optimization;

namespace GanttChart
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //style
            bundles.Add(new StyleBundle("~/Content/css/explorer")
                        .Include("~/Content/css/explorer.css",
                              Resources.GanttChart.GcIconsCssPath,
                              "~/Content/css/site.css"));
            bundles.Add(new StyleBundle("~/Content/appcss")
                        .Include("~/Content/css/app.css"));
            bundles.Add(new StyleBundle("~/BootStrapCss")
                        .Include("~/Content/css/bootstrap/css/bootstrap.css"));

            //script
            bundles.Add(new ScriptBundle("~/Content/js")
                        .Include("~/Scripts/jquery.js",
                              "~/Scripts/MultiLevelMenu.js",
                              "~/Scripts/Startup.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
