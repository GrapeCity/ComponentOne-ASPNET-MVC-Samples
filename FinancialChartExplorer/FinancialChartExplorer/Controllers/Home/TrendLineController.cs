using System.Collections.Generic;
using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult TrendLine()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreateTrendLineSettings() };
            return View(model);
        }

        private static IDictionary<string, object[]> CreateTrendLineSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Order", new object[]{"2","3","4","5","6","7","8","9"}},
                {"SampleCount", new object[]{"100","120","140","160","180","200"}},
                {"FitType", new object[]{ "Linear", "AverageX","AverageY","Exponential","Fourier","MaxX","MaxY","MinX","MinY","Polynomial"}},
            };

            return settings;
        }

    }
}
