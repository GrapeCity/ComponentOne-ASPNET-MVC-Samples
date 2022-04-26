using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using System.IO;

namespace FlexSheetExplorer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            OtherConfig();
        }

        public static void OtherConfig()
        {
            //TFS-415511: Create empty folder uploadFile which contains some sample files in server at runtime
            var uploadFilePath = AppDomain.CurrentDomain.BaseDirectory + "/Content/uploadFile";
            if (!Directory.Exists(uploadFilePath))
            {
                Directory.CreateDirectory(uploadFilePath);
            }
        }
    }
}
