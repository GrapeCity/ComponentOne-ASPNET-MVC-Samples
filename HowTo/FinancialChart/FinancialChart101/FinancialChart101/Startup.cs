using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialChart101.Startup))]
namespace FinancialChart101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
