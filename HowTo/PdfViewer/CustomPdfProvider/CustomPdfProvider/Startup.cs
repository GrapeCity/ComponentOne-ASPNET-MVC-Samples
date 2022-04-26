using System;
using Microsoft.Owin;
using Owin;
using CustomPdfProvider.Models;
using System.IO;

[assembly: OwinStartupAttribute(typeof(CustomPdfProvider.Startup))]
namespace CustomPdfProvider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStorageProviders().Add("CustomPdf", new CustomPdfStorageProvider());
        }
    }
}
