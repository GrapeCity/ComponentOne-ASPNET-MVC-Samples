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
        public ActionResult Renko()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreateRenkoSettings() };
            return View(model);
        }

        private static IDictionary<string, object[]> CreateRenkoSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Options.Renko.BoxSize", new object[]{"2","3","4","5","6","7","8","9","10","20"}},
                {"Options.Renko.RangeMode", new object[]{"ATR","Fixed"}},
                {"Options.Renko.Fields", new object[]{"High","Low","Open","Close","HL2","HLC3","HLOC4"}},
            };

            return settings;
        }
    }
}
