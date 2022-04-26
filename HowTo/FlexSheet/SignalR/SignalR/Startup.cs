using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalR.Startup))]
namespace SignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
