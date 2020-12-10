using C1.Web.Mvc.Chart;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;

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
                        {"SelectionMode", new object[] {SelectionMode.None, SelectionMode.Point, SelectionMode.Series }},
                        {"SelectedItemPosition", new object[] { Position.None, Position.Left, Position.Top, Position.Right, Position.Bottom, Position.Auto }},
                        {"SelectedItemOffset", new object[] {0, 0.1, 0.2, 0.3, 0.4, 0.5}},
                        {"IsAnimated", new object[] {false, true}}
                    }
            };

            return View(_data);
        }
    }
}
