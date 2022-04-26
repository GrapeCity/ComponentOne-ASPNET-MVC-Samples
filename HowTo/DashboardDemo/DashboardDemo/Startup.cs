using Microsoft.Owin;
using Owin;
using System.IO;

[assembly: OwinStartupAttribute(typeof(DashboardDemo.Startup))]
namespace DashboardDemo
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888


      app.UseReportProviders().AddFlexReportDiskStorage("Content", Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~"), "Content"));
    }
  }
}
