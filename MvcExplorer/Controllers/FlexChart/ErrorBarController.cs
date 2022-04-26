using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        List<PopulationByCountry> _populationByCountry = PopulationByCountry.GetData();
        public ActionResult ErrorBar()
        {
            var width = new object[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };
            var boolValues = new object[] { false, true };
            var settings = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"Direction", Enum.GetValues(typeof(ErrorBarDirection)).Cast<object>().ToArray()},
                    {"ErrorAmount", Enum.GetValues(typeof(ErrorAmount)).Cast<object>().ToArray()},
                    {"Value", new object[]{50, 100, 150, 200}},
                    {"EndStyle", Enum.GetValues(typeof(ErrorBarEndStyle)).Cast<object>().ToArray()}
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(_populationByCountry);
        }
    }
}
