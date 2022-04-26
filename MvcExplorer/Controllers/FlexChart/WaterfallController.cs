using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

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
