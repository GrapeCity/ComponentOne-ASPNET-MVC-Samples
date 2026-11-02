using Microsoft.Extensions.Configuration;

namespace MvcExplorer.Models
{
    public class AppSettings
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static string GetWebApiServiceUrl()
        {
            if (Configuration != null)
            {
                return Configuration["WebAPIService:Url"];
            }
            return null;
        }

        public static string WebRootPath { get; set; }
    }
}
