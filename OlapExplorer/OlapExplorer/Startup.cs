using Microsoft.Owin;
using OlapExplorer.Models;
using Owin;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(OlapExplorer.Startup))]
namespace OlapExplorer
{
    public partial class Startup
    {
        private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
        public void Configuration(IAppBuilder app)
        {
            app.UseDataEngineProviders()
                .AddDataEngine("complex10", () => ProductData.GetData(100000))
                .AddDataSource("dataset10", () => ProductData.GetData(100000).ToList())
                .AddCube("cube", @"Data Source=http://ssrs.componentone.com/OLAP/msmdpump.dll;Provider=msolap;Initial Catalog=AdventureWorksDW2012Multidimensional", "Adventure Works");
        }
    }
}
