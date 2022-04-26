using Microsoft.Owin;
using Owin;
using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(FlexViewerExplorer.Startup))]
namespace FlexViewerExplorer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var pdfsFolder = GetFullRoot("PdfsRoot");
            app.UseStorageProviders().AddDiskStorage("PdfsRoot", pdfsFolder);
            var reportsFolder = GetFullRoot("ReportsRoot");
            app.UseReportProviders().AddFlexReportDiskStorage("ReportsRoot", reportsFolder);

            var ssrsUrl = System.Configuration.ConfigurationManager.AppSettings["SsrsUrl"];
            var ssrsUserName = System.Configuration.ConfigurationManager.AppSettings["SsrsUserName"];
            var ssrsPassword = System.Configuration.ConfigurationManager.AppSettings["SsrsPassword"];
            app.UseReportProviders().AddSsrsReportHost("c1ssrs", ssrsUrl, new System.Net.NetworkCredential(ssrsUserName, ssrsPassword));
        }
        private static string GetFullRoot(string root)
        {
            var applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var fullRoot = Path.GetFullPath(Path.Combine(applicationBase, root));
            if (!fullRoot.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            {
                // When we do matches in GetFullPath, we want to only match full directory names.
                fullRoot += Path.DirectorySeparatorChar;
            }

            return fullRoot;
        }
    }
}
