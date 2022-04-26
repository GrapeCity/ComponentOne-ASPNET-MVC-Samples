using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: Accessibility
        public ActionResult Accessibility(FormCollection collection)
        {
            var settings = new ClientSettingsModel("tabAccessibility")
            {
                Settings = CreateAccessibilitySettings(),
                DefaultValues = GetAccessibilityDefaultValues()
            };
            ViewBag.DemoSettingsModel = settings;
            return View();
        }

        private static IDictionary<string, object[]> CreateAccessibilitySettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AutoSwitch", new object[]{false, true}}
            };

            return settings;
        }

        private static IDictionary<string, object> GetAccessibilityDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"AutoSwitch",true}
            };

            return defaultValues;
        }
    }
}