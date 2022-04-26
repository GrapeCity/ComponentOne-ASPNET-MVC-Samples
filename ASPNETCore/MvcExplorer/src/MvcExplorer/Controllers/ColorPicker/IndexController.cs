using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class ColorPickerController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ShowAlphaChannel", new object[]{true, false}},
                {"ShowColorString", new object[]{false, true}}
            };

            return settings;
        }
    }
}
