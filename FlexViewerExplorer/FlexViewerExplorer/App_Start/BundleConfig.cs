using System.Web;
using System.Web.Optimization;

namespace FlexViewerExplorer
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/SyntaxHighlighter/codeHighlight").Include(
                "~/Scripts/SyntaxHighlighter/shCore.js",
                "~/Scripts/SyntaxHighlighter/shBrushXml.js",
                "~/Scripts/SyntaxHighlighter/shBrushJScript.js",
                "~/Scripts/SyntaxHighlighter/shBrushCSharp.js"));
            bundles.Add(new StyleBundle("~/Content/SyntaxHighlighter/codeHighlight").Include(
                "~/Content/css/SyntaxHighlighter/shCore.css",
                "~/Content/css/SyntaxHighlighter/shCoreEclipse.css"));

            bundles.Add(new ScriptBundle("~/bootstrapJS").Include(
                "~/Scripts/jquery.js",
                "~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/bootstrapCSS").Include(
                "~/Content/bootstrap/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/explorer").Include(
                "~/Content/css/explorer.css", Resources.FlexViewer.GcIconsCssPath, "~/Content/css/app.css"));
            bundles.Add(new StyleBundle("~/siteCSS").Include(
                "~/Content/css/site.css"));
            bundles.Add(new ScriptBundle("~/siteJS").Include(
                "~/Scripts/site.js"));
        }
    }
}
