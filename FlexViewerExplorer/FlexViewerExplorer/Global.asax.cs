using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FlexViewerExplorer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(config);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            config.EnsureInitialized();
        }
    }
}
