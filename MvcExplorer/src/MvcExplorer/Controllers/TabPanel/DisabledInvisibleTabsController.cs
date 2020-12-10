using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: DisabledInvisibleTabs
        public ActionResult DisabledInvisibleTabs(IFormCollection collection)
        {
            var settings = new ClientSettingsModel("tabDisableHide")
            {
                Settings = CreateDisabledInvisibleSettings(),
                DefaultValues = GetDisabledInvisibleDefaultValues()
            };
            ViewBag.DemoSettingsModel = settings;
            return View();
        }

        private static IDictionary<string, object[]> CreateDisabledInvisibleSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"DisableEurope", new object[]{false, true}},
                {"ShowOceania", new object[]{false, true}},
            };

            return settings;
        }

        private static IDictionary<string, object> GetDisabledInvisibleDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"DisableEurope",true},
                {"ShowOceania",true},
            };

            return defaultValues;
        }
    }
}