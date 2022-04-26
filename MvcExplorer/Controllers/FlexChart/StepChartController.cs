using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult StepChart()
        {
            var data = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"ChartType", new object[]{"Step", "StepSymbols", "StepArea"}},
                    {"StepPosition", new object[] {"Start", "Center", "End"}}
                }
            };
            data.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = data;
            return View(StatisticMessage.GetData());
        }
    }
}