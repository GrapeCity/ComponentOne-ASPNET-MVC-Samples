using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using C1.Web.Api.DataEngine;
using C1.Web.Api.Storage;

namespace DashboardDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
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
            var customControllersAssembly = typeof(StorageController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            customControllersAssembly = typeof(DataEngineController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
            {
                assemblies.Add(customControllersAssembly);
            }

            return assemblies;
        }
    }
}
