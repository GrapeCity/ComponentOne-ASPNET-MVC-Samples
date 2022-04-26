using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
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
                {"DataLabel.Position", new object[]{LabelPosition.Top, LabelPosition.Right, LabelPosition.Bottom, LabelPosition.Left, LabelPosition.None}},
                {"DataLabel.Border", new object[]{false, true}},
            };

            return settings;
        }
    }
}
