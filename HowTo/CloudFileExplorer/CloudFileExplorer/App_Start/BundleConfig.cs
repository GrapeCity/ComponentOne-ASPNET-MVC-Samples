using System.Web;
using System.Web.Optimization;

namespace CloudFileExplorer
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.Add(new ScriptBundle("~/bundles/explorer").Include(
					"~/Scripts/MultiLevelMenu.js", "~/Scripts/Layout.js"));
			bundles.Add(new ScriptBundle("~/bundles/SyntaxHighlighter/codeHighlight").Include(
					"~/Scripts/SyntaxHighlighter/shCore.js",
					"~/Scripts/SyntaxHighlighter/shBrushXml.js",
					"~/Scripts/SyntaxHighlighter/shBrushJScript.js",
					"~/Scripts/SyntaxHighlighter/shBrushCSharp.js"));
			bundles.Add(new StyleBundle("~/Content/css/explorer").Include(
					"~/Content/css/explorer.css", Resources.CloudFileExplorer.GcIconsCssPath, "~/Content/css/site.css"));
			bundles.Add(new StyleBundle("~/Content/appcss").Include(
					"~/Content/css/app.css"));
			bundles.Add(new StyleBundle("~/Content/SyntaxHighlighter/codeHighlight").Include(
					"~/Content/css/SyntaxHighlighter/shCore.css",
					"~/Content/css/SyntaxHighlighter/shCoreEclipse.css"));
			bundles.Add(new StyleBundle("~/BootStrapCss").Include(
					"~/Content/css/bootstrap/css/bootstrap.css"));
			bundles.Add(new ScriptBundle("~/jquery").Include(
									"~/Scripts/jquery-{version}.js"));
			bundles.Add(new ScriptBundle("~/jqueryval").Include(
									"~/Scripts/jquery.validate*"));
			bundles.Add(new ScriptBundle("~/BootStrapJS").Include(
					"~/Scripts/BootStrap/bootstrap.js"));
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
												"~/Content/font-awesome.css",
												"~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
