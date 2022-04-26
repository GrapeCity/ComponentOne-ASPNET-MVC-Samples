using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexChartExport.Models;

namespace FlexChartExport.Controllers
{
    public class HomeController : Controller
    {
        //Controller Action Method for Index.cshtml
        public ActionResult Index()
        {
            FlexChartModel ModelObj = new FlexChartModel();
            ModelObj.Settings = CreateIndexSettings();
            ModelObj.CountrySalesData = CountryData.GetCountryData();
            return View(ModelObj);
        }

        private IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"ExportTo", new object[]{"png", "jpeg", "svg"}}
            };
            return settings;
        }
    }
}
