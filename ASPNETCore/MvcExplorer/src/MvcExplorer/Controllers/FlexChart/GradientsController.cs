using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Gradients()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"Fill", new object[]{
                        "Light Blue - l(0,0,1,0)#89f7fe-#66a6ff",
                        "Aqua - l(0,0,0,1)#13547a-#80d0c7",
                        "Red - l(0, 0, 1, 1)#ff0844-#ffb199",
                        "Purple - l(0, 0, 1, 0)#b224ef-#7579ff-#b224ef",
                        "Plum - r(0.5,0.5,0.7)#cc208e-#6713d2",
                        "Deep Blue - l(0, 0, 1, 0)#30cfd0-#330867",
                        "Orange - l(0, 0, 0, 1)#e27f00-#ae1a73",
                        "Green - l(0, 0, 1, 1)#abd800-#5c7e00"
                    } }
                }
            };
            return View(Fruit.GetFruitsSales());
        }
    }
}
