using Microsoft.Owin;
using Olap101.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(Olap101.Startup))]
namespace Olap101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseDataEngineProviders()
                .AddDataEngine("complex10", () => ProductData.GetData(100000));
        }
    }
}
