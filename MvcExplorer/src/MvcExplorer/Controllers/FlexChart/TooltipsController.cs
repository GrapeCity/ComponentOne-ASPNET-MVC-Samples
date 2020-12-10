using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Tooltips()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"Style", new object[]{
                        "green-white-tooltip",
                        "red-yellow-tooltip",
                        "gradient-tooltip",
                        "text-style-tooltip",
                        "strong-round-tooltip",
                        "default-tooltip"
                    } }
                }
            };

            return View(Fruit.GetFruitsSales());
        }
    }
}
