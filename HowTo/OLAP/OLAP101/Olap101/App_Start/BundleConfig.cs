using System.Web;
using System.Web.Optimization;

namespace Olap101
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/jszip.js", "~/Scripts/app.js"));
        }
    }
}
