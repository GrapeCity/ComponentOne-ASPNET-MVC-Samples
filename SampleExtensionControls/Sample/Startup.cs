using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proj_CustomEditor.Startup))]
namespace proj_CustomEditor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
