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
        List<SalesAnalysis> _analysisData = SalesAnalysis.GetData();
        public ActionResult BoxWhisker()
        {
            var width = new object[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            var boolValues = new object[] { false, true };
            var settings = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupWidth", width},
                    {"GapWidth", width},
                    {"ShowMeanLine", boolValues},
                    {"ShowMeanMarker", boolValues},
                    {"ShowInnerPoints", boolValues},
                    {"ShowOutliers", boolValues},
                    {"Rotated", boolValues},
                    {"ShowLabel", boolValues},
                    {"QuartileCalculation", Enum.GetValues(typeof(C1.Web.Mvc.Chart.QuartileCalculation)).Cast<object>().ToArray()}
                },
                DefaultValues = new Dictionary<string, object>
                {
                    {"GapWidth", "0.1"},
                    {"GroupWidth", "0.8"}
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(_analysisData);
        }
    }
}
