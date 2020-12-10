using C1.Web.Mvc.Olap;
using Microsoft.AspNetCore.Mvc;
using OlapExplorer.Models;
using System.Collections.Generic;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        private static Dictionary<string, object[]> ChartSettings = new Dictionary<string, object[]>
        {
            {"ChartType", new object[] { PivotChartType.Column, PivotChartType.Area, PivotChartType.Bar, PivotChartType.Line, PivotChartType.Pie, PivotChartType.Scatter} }
        };

        // GET: PivotGrid
        public ActionResult DataEngine()
        {
            var engineModel = new ClientSettingsModel { Settings = new Dictionary<string, object[]>() };
            foreach (var item in OlapModel.Settings)
            {
                engineModel.Settings.Add(item);
            }
            foreach (var chartItem in ChartSettings)
            {
                engineModel.Settings.Add(chartItem);
            }
            engineModel.ControlId = "chart";
            ViewBag.DemoOptions = engineModel;
            return View();
        }
    }
}