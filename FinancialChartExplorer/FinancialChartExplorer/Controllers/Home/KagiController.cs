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
        public ActionResult Kagi()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreateKagiSettings() };
            return View(model);
        }

        private static IDictionary<string, object[]> CreateKagiSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Options.Kagi.ReversalAmount", new object[]{"2","3","4","5","6","7","8","9","10"}},
                {"Options.Kagi.RangeMode", new object[]{"Fixed","ATR","Percentage"}},
                {"Options.Kagi.Fields", new object[]{"High","Low","Open","Close","HighLow","HL2","HLC3","HLOC4"}},
            };

            return settings;
        }
    }
}
