using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        List<PopulationByCountry> _populationByCountry = PopulationByCountry.GetData();
        public ActionResult ErrorBar()
        {
            var settings = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"Direction", new object[] { ErrorBarDirection.Both, ErrorBarDirection.Minus, ErrorBarDirection.Plus } },
                    {"ErrorAmount", new object[] { ErrorAmount.FixedValue, ErrorAmount.Custom, ErrorAmount.Percentage,
                        ErrorAmount.StandardDeviation, ErrorAmount.StandardError } },
                    {"Value", new object[] { 50, 100, 150, 200 } },
                    {"EndStyle", new object[] { ErrorBarEndStyle.Cap, ErrorBarEndStyle.NoCap } }
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(_populationByCountry);
        }
    }
}
