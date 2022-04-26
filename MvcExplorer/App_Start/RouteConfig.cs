using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcExplorer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Validation",
                url: "{control}/UnobtrusiveValidation",
                defaults: new { controller = "Validation", action = "Register" },
                constraints: new { control = "(AutoComplete)|(ComboBox)|(MultiSelect)|(^Input.*)|(MultiAutoComplete)" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}