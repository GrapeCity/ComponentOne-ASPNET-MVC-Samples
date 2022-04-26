using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiSelection.Models;

namespace MultiSelection.Controllers
{
    public class HomeController : Controller
    {
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
                {"ChartType", new object[]{"Column", "Bar", "LineSymbols", "SplineSymbols", "Scatter"}},
                {"SelectList", new object[]{"None","500000", "1000000", "1500000", "2000000"}}
            };

            return settings;
        }        
    }
}