using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Chart;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class SunburstController : Controller
    {
        public ActionResult LegendAndTitles()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                    {
                        {"Legend.Position", Enum.GetValues(typeof(Position)).Cast<object>().ToArray()},
                        {"Header", new object[] {"Header", "ヘッダー"}},
                        {"Footer", new object[] {"Footer", "フッター"}}
                    },
                DefaultValues = new Dictionary<string, object>
                    {
                        {"Legend.Position", Position.Right}
                    }
            };

            return View(_data);
        }
    }
}
