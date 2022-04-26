using System.Web;
using System.Web.Optimization;

namespace FinancialChartExplorer
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/BootStrapCss").Include(
                "~/Content/css/bootstrap/css/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/SyntaxHighlighter/codeHighlight").Include(
                "~/Content/css/SyntaxHighlighter/shCore.css",
                "~/Content/css/SyntaxHighlighter/shCoreEclipse.css"));
            bundles.Add(new StyleBundle("~/Content/css/explorer").Include(
                "~/Content/css/explorer.css",
                Resources.Resource.GcIconsCssPath,
                "~/Content/css/site.css"));

            bundles.Add(new ScriptBundle("~/BootStrapJS").Include(
                "~/Scripts/BootStrap/jquery.js",
                "~/Scripts/BootStrap/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/SyntaxHighlighter/codeHighlight").Include(
                "~/Scripts/SyntaxHighlighter/shCore.js",
                "~/Scripts/SyntaxHighlighter/shBrushXml.js",
                "~/Scripts/SyntaxHighlighter/shBrushJScript.js",
                "~/Scripts/SyntaxHighlighter/shBrushCSharp.js"));
            bundles.Add(new ScriptBundle("~/bundles/explorer").Include(
                "~/Scripts/MultiLevelMenu.js",
                "~/Scripts/Layout.js",
                "~/Scripts/explorer.js"));
        }
    }
}