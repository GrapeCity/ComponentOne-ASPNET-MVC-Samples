using System;
using Microsoft.Owin;
using Owin;
using C1.Web.Api.Storage;
using CustomReportProvider.Models;

[assembly: OwinStartupAttribute(typeof(CustomReportProvider.Startup))]
namespace CustomReportProvider
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseReportProviders().Add("memoryreports", new MemoryReportsProvider());
        }
    }
}
