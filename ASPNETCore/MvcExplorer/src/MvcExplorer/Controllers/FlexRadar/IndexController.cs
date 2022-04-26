using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexRadarController : Controller
    {
        private List<ProductSales> productSales = ProductSales.GetData();
        public ActionResult Index()
        {
            var settings = new ClientSettingsModel
            {
                Settings = CreateRadarSettings(),
                DefaultValues = GetIndexDefaultValues()
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(productSales);
        }

        private static IDictionary<string, object[]> CreateRadarSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Scatter", "Line", "LineSymbols", "Area"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked100pc"}},
                {"StartAngle", new object[]{0, 60, 120, 180, 240, 300, 360}},
                {"TotalAngle", new object[]{60, 120, 180, 240, 300, 360}},
                {"Reversed", new object[]{false, true}}
            };

            return settings;
        }

        private static IDictionary<string, object> GetIndexDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"TotalAngle", 360}
            };

            return defaultValues;
        }

    }
}
