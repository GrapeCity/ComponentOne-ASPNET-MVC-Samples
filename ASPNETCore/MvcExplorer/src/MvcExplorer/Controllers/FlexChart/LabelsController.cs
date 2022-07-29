using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Labels()
        {
            var model = new ClientSettingsModel
            {
                Settings = CreateLabelSettings()
            };

            return View(model);
        }

        private static IDictionary<string, object[]> CreateLabelSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"DataLabel.Position", new object[]{LabelPosition.Top.ToString(), LabelPosition.Right.ToString(), LabelPosition.Bottom.ToString(), LabelPosition.Left.ToString(), LabelPosition.Center.ToString(), LabelPosition.None.ToString()}},
                {"DataLabel.Border", new object[]{false, true}},
            };

            return settings;
        }
    }
}
