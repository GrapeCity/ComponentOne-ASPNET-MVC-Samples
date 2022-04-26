using System.Web;
using System.Web.Optimization;

namespace DashboardDemo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/explorer").Include(
    "~/Content/css/explorer.css"));
            bundles.Add(new StyleBundle("~/Content/css/bootstrap/css/BootStrapCss").Include(
                "~/Content/css/bootstrap/css/bootstrap.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
