using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;
using C1.Web.Mvc.Chart;
using System;

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
                        {"Legend.Position", new object[] { Position.None, Position.Left, Position.Top, Position.Right, Position.Bottom}},
                        {"Header", new object[] {"Header", "ヘッダー"}},
                        {"Footer", new object[] {"Footer", "フッター"}}
                    }
            };

            return View(_data);
        }
    }
}
