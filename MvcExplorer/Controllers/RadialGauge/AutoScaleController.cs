using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class RadialGaugeController : Controller
    {
        public ActionResult AutoScale()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateAutoScaleSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateAutoScaleSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"SweepAngle", new object[]{90, 180, 270, 360}},
                {"StartAngle", new object[]{0, 90, 180, 270, 360}},
                {"AutoScale", new object[]{true, false}}
            };

            return settings;
        }
    }
}
