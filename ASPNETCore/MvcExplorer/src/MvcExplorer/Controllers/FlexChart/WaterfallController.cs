using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        List<WaterfallData> _data = WaterfallData.GetData();
        public ActionResult Waterfall()
        {
            var settings = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"RelativeData", new object[] {false, true}},
                    {"ConnectorLines", new object[] {true, false}},
                    {"ShowTotal", new object[] {true, false}},
                    {"ShowIntermediateTotal", new object[] {false, true}}
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(_data);
        }
    }
}
