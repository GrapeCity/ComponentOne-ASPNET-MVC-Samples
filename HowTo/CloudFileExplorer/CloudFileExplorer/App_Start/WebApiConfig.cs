using C1.Web.Api;
using C1.Web.Api.Storage;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CloudFileExplorer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Use APIs defined in the C1 library
            config.Services.Replace(typeof(IAssembliesResolver), new CustomAssembliesResolver());

            // Web API routes
            config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
    }
    internal class CustomAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<System.Reflection.Assembly> GetAssemblies()
        {
            var assemblies = base.GetAssemblies();
            var customControllersAssembly = typeof(ExporterResult).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

			customControllersAssembly = typeof(StorageController).Assembly;
			if (!assemblies.Contains(customControllersAssembly))
			{
				assemblies.Add(customControllersAssembly);
			}
			return assemblies;
        }
    }
}