using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        private List<SalesData> _salesData = SalesData.GetData();
        public ActionResult Funnel()
        {
            var settings = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"FunnelType", new object[]{"Default", "Rectangle"}},
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;
            return View(_salesData);
        }
    }
}
