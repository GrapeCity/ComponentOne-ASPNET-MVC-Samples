using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class RadialGaugeController : Controller
    {
        public ActionResult ShowText()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateShowTextSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateShowTextSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ShowText", new object[]{"All", "Value", "MinMax", "None"}}
            };

            return settings;
        }
    }
}
