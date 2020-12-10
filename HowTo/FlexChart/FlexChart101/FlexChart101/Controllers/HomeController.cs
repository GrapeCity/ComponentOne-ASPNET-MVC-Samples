using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FlexChart101.Models;
using C1.Web.Mvc.Chart;

namespace FlexChart101.Controllers
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
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked 100%"}},
                {"Rotated", new object[]{false.ToString(), true.ToString()}},
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"GroupWidth", new object[]{"25%", "70%", "100%", "50 pixels"}},
                {"Position",new object[]{Position.None.ToString(),Position.Left.ToString(),Position.Top.ToString(),Position.Right.ToString(),Position.Bottom.ToString()}},
                {"SelectionMode",new object[]{SelectionMode.None.ToString(),SelectionMode.Series.ToString(),SelectionMode.Point.ToString()}}
            };

            return settings;
        }
    }
}
