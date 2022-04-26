using System.Web;
using System.Web.Optimization;

namespace HeaderFilters
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/appcss").Include(
                "~/Content/bootstrap/css/bootstrap.css",
                "~/Content/app.css"));
        }
    }
}