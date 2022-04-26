using System;
using Microsoft.Owin;
using Owin;
using System.IO;

[assembly: OwinStartupAttribute(typeof(PdfViewer.Startup))]
namespace PdfViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStorageProviders().AddDiskStorage("UploadFiles", GetFullRoot("UploadFiles"));
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
