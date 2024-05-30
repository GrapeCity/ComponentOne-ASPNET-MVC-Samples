using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        //
        // GET: /FlexChart/
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
                {"ChartType", new object[]{"Line", "LineSymbols", "Area"}},
                {"InterpolateNulls", new object[]{true, false}},
                {"LegendToggle", new object[]{true, false}},
                {"AxisY.LabelMin", new object[]{false, true}},
                {"AxisY.LabelMax", new object[]{false, true}},
            };

            return settings;
        }
    }
}
