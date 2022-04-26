using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
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
