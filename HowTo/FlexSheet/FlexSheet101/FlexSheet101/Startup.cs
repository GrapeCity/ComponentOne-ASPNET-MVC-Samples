using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlexSheet101.Startup))]
namespace FlexSheet101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
