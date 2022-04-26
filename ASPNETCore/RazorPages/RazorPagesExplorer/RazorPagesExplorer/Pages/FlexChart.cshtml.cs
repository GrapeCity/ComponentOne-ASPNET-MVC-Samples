using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesExplorer.Models;

namespace RazorPagesExplorer.Pages
{
    public class FlexChartModel : PageModel
    {
        public readonly IEnumerable<Fruit> FruitsSales = Fruit.GetFruitsSales();
        public void OnGet()
        {
            ViewData["DemoSettingsModel"] = new ClientSettingsModel
            {
                Settings = CreateIndexSettings(),
                DefaultValues = GetIndexDefaultValues()
            };
        }

        private static IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked 100%"}},
                {"Rotated", new object[]{false, true}},
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"GroupWidth", new object[]{"25%", "70%", "100%", "50 pixels"}}
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