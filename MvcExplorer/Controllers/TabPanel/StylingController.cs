using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: Styling
        public ActionResult Styling(FormCollection collection)
        {
            var settings = new ClientSettingsModel("tabCss")
            {
                Settings = CreateStylingSettings(),
                DefaultValues = GetStylingDefaultValues()
            };
            ViewBag.DemoSettingsModel = settings;
            return View();
        }

        private static IDictionary<string, object[]> CreateStylingSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"TabAlignment", new object[]{"Left", "Center", "Right"}},
                {"IsAnimated", new object[]{ false, true} },
                {"CustomHeaders", new object[]{ false, true} },
                {"TabPosition", new object[]{ "Above", "Left", "Right", "Below" } }
            };

            return settings;
        }

        private static IDictionary<string, object> GetStylingDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"IsAnimated",true}
            };

            return defaultValues;
        }
    }
}