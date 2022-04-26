using MvcExplorer.Models;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace MvcExplorer
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Order>("Orders");
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "MyNorthWind", //"odata",
                model: builder.GetEdmModel());
        }
    }
}