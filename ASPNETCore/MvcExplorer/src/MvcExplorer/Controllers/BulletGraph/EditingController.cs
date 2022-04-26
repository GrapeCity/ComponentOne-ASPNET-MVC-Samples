using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class BulletGraphController : Controller
    {
        public ActionResult Editing()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateEditingSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateEditingSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"IsReadOnly", new object[]{false, true }},
                {"Step", new object[]{0.5, 1, 2}},
                {"HandleWheel", new object[]{true, false }}
            };

            return settings;
        }
    }
}
