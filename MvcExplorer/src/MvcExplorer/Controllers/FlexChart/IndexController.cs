using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController: Controller
    {
        public ActionResult Index()
        {
            var settings = new ClientSettingsModel
            {
                Settings = CreateIndexSettings(),
                DefaultValues = GetIndexDefaultValues()
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;

            return View(Fruit.GetFruitsSales());
        }

        private static IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked 100%"}},
                {"Rotated", new object[]{false, true}},
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"GroupWidth", new object[]{"25%", "70%", "100%", "50 pixels"}},
                {"LegendOrientation", new object[]{ "Auto", "Vertical", "Horizontal"}},
                {"TooltipPosition", new object[]{
                        "Above",
                        "AboveRight",
                        "RightTop",
                        "Right",
                        "RightBottom",
                        "BelowRight",
                        "Below",
                        "BelowLeft",
                        "LeftBottom",
                        "Left",
                        "LeftTop",
                        "AboveLeft"
                    }}
            };

            return settings;
        }

        private static IDictionary<string, object> GetIndexDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"GroupWidth", "70%"}
            };

            return defaultValues;
        }
    }
}
