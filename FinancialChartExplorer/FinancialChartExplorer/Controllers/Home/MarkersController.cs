using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialChartExplorer.Models;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Markers()
        {
            var model = BoxData.GetDataFromJson().GetRange(0, 20);
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreateMarkersSettings() };
            return View(model);
        }

        private static IDictionary<string, object[]> CreateMarkersSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Alignment", new object[]{"Auto","Bottom","Left","Right","Top"}},
                {"Interaction", new object[]{"Move","Drag","None"}},
                {"Lines", new object[]{"Both","Horizontal","Vertical","None"}},
            };

            return settings;
        }
    }
}
