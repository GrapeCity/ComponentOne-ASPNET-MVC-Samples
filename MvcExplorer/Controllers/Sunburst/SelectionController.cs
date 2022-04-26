using System;
using System.Collections.Generic;
using System.Web.Mvc;
using C1.Web.Mvc.Chart;
using MvcExplorer.Models;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class SunburstController : Controller
    {
        public ActionResult Selection()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                    {
                        {"SelectionMode", Enum.GetValues(typeof(SelectionMode)).Cast<object>().ToArray()},
                        {"SelectedItemPosition", Enum.GetValues(typeof(Position)).Cast<object>().ToArray()},
                        {"SelectedItemOffset", new object[] {0, 0.1, 0.2, 0.3, 0.4, 0.5}},
                        {"IsAnimated", new object[] {false, true}}
                    }
            };

            return View(_data);
        }
    }
}
