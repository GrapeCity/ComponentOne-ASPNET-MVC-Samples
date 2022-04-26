using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
