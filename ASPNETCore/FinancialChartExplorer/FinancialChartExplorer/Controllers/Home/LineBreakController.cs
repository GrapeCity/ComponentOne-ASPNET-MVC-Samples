using System.Collections.Generic;
using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult LineBreak()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreateLineBreakSettings() };
            return View(model);
        }

        private static IDictionary<string, object[]> CreateLineBreakSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"LineBreak", new object[]{"1","2","3","4","5","6"}},
                {"Stroke", new object[]{"LightBlue","Red", "Green", "Blue","Black","Purple","Yellow","Orange","Silver","Brown"}},
                {"AltStroke", new object[]{"LightBlue","Red", "Green", "Blue","Black","Purple","Yellow","Orange","Silver","Brown"}},
                {"Fill", new object[]{"LightBlue","Transparent","Red", "Green", "Blue","Black","Purple","Yellow","Orange","Silver","Brown"}},
                {"AltFill ", new object[]{"Transparent","LightBlue","Red", "Green", "Blue","Black","Purple","Yellow","Orange","Silver","Brown"}}
            };

            return settings;
        }

    }
}
