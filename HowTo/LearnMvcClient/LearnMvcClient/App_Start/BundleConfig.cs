using System.Web.Optimization;

namespace LearnMvcClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // styles
            bundles.Add(new StyleBundle("~/Content/css/explorer")
                .Include("~/Content/css/explorer.css",
                        Resources.Resource.GcIconsCssPath,
                        "~/Content/css/site.css"));
            bundles.Add(new StyleBundle("~/Content/appcss")
                .Include("~/Content/css/app.css"));
            bundles.Add(new StyleBundle("~/Content/SyntaxHighlighter/codeHighlight")
                .Include("~/Content/css/SyntaxHighlighter/shCore.css",
                        "~/Content/css/SyntaxHighlighter/shCoreEclipse.css"));
            bundles.Add(new StyleBundle("~/BootStrapCss")
                .Include("~/Content/css/bootstrap/css/bootstrap.css"));

            // scripts
            bundles.Add(new ScriptBundle("~/bundles/SyntaxHighlighter/codeHighlight")
                .Include("~/Scripts/SyntaxHighlighter/shCore.js",
                        "~/Scripts/SyntaxHighlighter/shBrushXml.js",
                        "~/Scripts/SyntaxHighlighter/shBrushJScript.js",
                        "~/Scripts/SyntaxHighlighter/shBrushCSharp.js"));
            bundles.Add(new ScriptBundle("~/bundles/sample")
                .Include("~/Scripts/jquery.js",
                        "~/Scripts/Utility.js",
                        "~/Scripts/MultiLevelMenu.js",
                        "~/Scripts/DropDownMenu.js",
                        "~/Scripts/Tabs.js",
                        "~/Scripts/Startup.js"));
        }
    }
}
