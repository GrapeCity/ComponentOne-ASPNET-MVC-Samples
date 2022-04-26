using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexRadarController : Controller
    {
        private List<MapData> mapData = MapData.GetData();
        public ActionResult Polar()
        {
            var settings = new ClientSettingsModel
            {
                Settings = CreateRadarSettings(),
                DefaultValues = GetPolarDefaultValues()
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(mapData);
        }

        private static IDictionary<string, object> GetPolarDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"ChartType", "Line"},
                {"TotalAngle", 360}
            };

            return defaultValues;
        }
    }
}
