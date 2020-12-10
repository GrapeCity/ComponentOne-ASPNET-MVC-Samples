using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult Index()
        {
            var settings = new ClientSettingsModel
            {
                Settings = CreateSettings(),
                DefaultValues = new Dictionary<string, object>
                {
                    {"DataLabel.Position", PieLabelPosition.Center}
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(CustomerOrder.GetCountryGroupOrderData());
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"InnerRadius", new object[]{0, 0.25, 0.5, 0.75}},
                {"Offset", new object[]{0, 0.1, 0.2, 0.3, 0.4, 0.5}},
                {"StartAngle", new object[]{0, 90, 180, -90}},
                {"Reversed", new object[]{false, true}},
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"DataLabel.Position", new object[]{PieLabelPosition.None, PieLabelPosition.Inside, PieLabelPosition.Center, PieLabelPosition.Outside}},
                {"DataLabel.Border", new object[]{false, true}},
            };

            return settings;
        }
    }
}