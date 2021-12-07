using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Chart;

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
            ViewBag.Palettes = GetPalettes();

            return View(Fruit.GetFruitsSales());
        }

        private static IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Stacking", new object[]{"None", "Stacked", "Stacked 100%"}},
                {"Rotated", new object[]{false, true}},
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
                    }},
                {"MaxSize", new object[] {"50px", "10%", "100%", "20px", "0.5%", "50%", "100px" } }
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

        private object[] GetPalettes()
        {
            return new object[] {
                new {Header = "Standard", Value = Palettes.Standard},
                new {Header = "Cocoa", Value = Palettes.Cocoa},
                new {Header = "Coral", Value = Palettes.Coral},
                new {Header = "Dark", Value = Palettes.Dark},
                new {Header = "Highcontrast", Value = Palettes.Highcontrast},
                new {Header = "Light", Value = Palettes.Light},
                new {Header = "Midnight", Value = Palettes.Midnight},
                new {Header = "Minimal", Value = Palettes.Minimal},
                new {Header = "Modern", Value = Palettes.Modern },
                new {Header = "Organic", Value = Palettes.Organic },
                new {Header = "Slate", Value = Palettes.Slate },
                new {
                    Header = "Qualitative",
                    Items = new object[]
                    {
                        new {Header = "Accent", GroupName = "Qualitative", Value = Palettes.Qualitative.Accent},
                        new {Header = "Dark2", GroupName = "Qualitative", Value = Palettes.Qualitative.Dark2},
                        new {Header = "Paired", GroupName = "Qualitative", Value = Palettes.Qualitative.Paired},
                        new {Header = "Pastel1", GroupName = "Qualitative", Value = Palettes.Qualitative.Pastel1},
                        new {Header = "Pastel2", GroupName = "Qualitative", Value = Palettes.Qualitative.Pastel2},
                        new {Header = "Set1", GroupName = "Qualitative", Value = Palettes.Qualitative.Set1},
                        new {Header = "Set2", GroupName = "Qualitative", Value = Palettes.Qualitative.Set2},
                        new {Header = "Set3", GroupName = "Qualitative", Value = Palettes.Qualitative.Set3},
                    }
                },
                new {
                    Header = "Diverging",
                    Items = new object[]
                    {
                        new {Header = "BrBG", GroupName = "Diverging", Value = Palettes.Diverging.BrBG},
                        new {Header = "PiYG", GroupName = "Diverging", Value = Palettes.Diverging.PiYG},
                        new {Header = "PRGn", GroupName = "Diverging", Value = Palettes.Diverging.PRGn},
                        new {Header = "PuOr", GroupName = "Diverging", Value = Palettes.Diverging.PuOr},
                        new {Header = "RdBu", GroupName = "Diverging", Value = Palettes.Diverging.RdBu},
                        new {Header = "RdGy", GroupName = "Diverging", Value = Palettes.Diverging.RdGy},
                        new {Header = "RdYlBu", GroupName = "Diverging", Value = Palettes.Diverging.RdYlBu},
                        new {Header = "RdYlGn", GroupName = "Diverging", Value = Palettes.Diverging.RdYlGn},
                        new {Header = "Spectral",GroupName = "Diverging", Value = Palettes.Diverging.Spectral},
                    }
                },
                new {
                    Header = "SequentialSingle",
                    Items = new object[]
                    {
                        new {Header = "Blues", GroupName = "SequentialSingle", Value= Palettes.SequentialSingle.Blues},
                        new {Header = "Greens",GroupName = "SequentialSingle", Value= Palettes.SequentialSingle.Greens},
                        new {Header = "Greys",GroupName = "SequentialSingle", Value= Palettes.SequentialSingle.Greys},
                        new {Header = "Oranges",GroupName = "SequentialSingle", Value= Palettes.SequentialSingle.Oranges},
                        new {Header = "Purples", GroupName = "SequentialSingle", Value= Palettes.SequentialSingle.Purples},
                        new {Header = "Reds",GroupName = "SequentialSingle", Value= Palettes.SequentialSingle.Reds},
                    }
                },
                new {
                    Header = "SequentialMulti",
                    Items = new object[]
                    {
                        new { Header = "BuGn", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.BuGn},
                        new {Header = "BuPu", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.BuPu},
                        new {Header = "GnBu",GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.GnBu},
                        new {Header = "OrRd", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.OrRd},
                        new {Header = "PuBu", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.PuBu},
                        new {Header = "PuBuGn", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.PuBuGn},
                        new {Header = "PuRd", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.PuRd},
                        new {Header = "RdPu",GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.RdPu},
                        new {Header = "YlGn",GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.YlGn},
                        new {Header = "YlGnBu", GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.YlGnBu},
                        new {Header = "YlOrBr",GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.YlOrBr},
                        new {Header = "YlOrRd",GroupName = "SequentialMulti", Value= Palettes.SequentialMulti.YlOrRd}
                    }
                }
            };
        }
    }
}
