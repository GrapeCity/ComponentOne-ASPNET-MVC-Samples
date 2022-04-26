using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace FlexSheet101.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory(
                        "~/Scripts/app", "*.js"));
            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
        }
    }
}
