using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        private Fruit _apple = new Fruit();
        public ActionResult Binding()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateBindingSettings()
            };

            return View(_apple.Sales);
        }

        private static IDictionary<string, object[]> CreateBindingSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                { "ChartType", new object[] { "Line", "LineSymbols", "Area"} },
                { "InterpolateNulls", new object[] { true, false} },
                { "LegendToggle", new object[] { true, false } }
            };
            return settings;
        }
    }
}
