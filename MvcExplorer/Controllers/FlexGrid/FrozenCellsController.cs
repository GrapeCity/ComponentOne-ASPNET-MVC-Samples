using System.Collections.Generic;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult FrozenCells()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateSettings()
            };
            return View(Sale.GetData(500));
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"FrozenColumns", new object[]{1, 0, 2, 3}},
                {"FrozenRows", new object[]{2, 0, 1, 3, 4, 5}}
            };

            return settings;
        }
    }
}
