using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class BulletGraphController : Controller
    {
        public ActionResult Ranges()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateShowRangesSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateShowRangesSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ShowRanges", new object[]{true, false}}
            };

            return settings;
        }
    }
}
