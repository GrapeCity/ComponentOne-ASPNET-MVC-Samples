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
