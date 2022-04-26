using System.Web.Optimization;

namespace ImageCombo
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/SyntaxHighlighter/codeHighlight").Include(
                "~/Scripts/SyntaxHighlighter/shCore.js",
                "~/Scripts/SyntaxHighlighter/shBrushXml.js",
                "~/Scripts/SyntaxHighlighter/shBrushJScript.js",
                "~/Scripts/SyntaxHighlighter/shBrushCSharp.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/layout.css", "~/Content/css/site.css"));
            bundles.Add(new StyleBundle("~/Content/SyntaxHighlighter/codeHighlight").Include(
                "~/Content/css/SyntaxHighlighter/shCore.css",
                "~/Content/css/SyntaxHighlighter/shCoreEclipse.css"));
        }
    }
}