using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;
using C1.Web.Mvc.Chart;
using System;
using System.Linq;

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
                        {"Legend.Position", Enum.GetValues(typeof(Position)).Cast<object>().ToArray() },
                        {"Header", new object[] {"Header", "ヘッダー"}},
                        {"Footer", new object[] {"Footer", "フッター"}}
                    }
            };

            return View(_data);
        }
    }
}
