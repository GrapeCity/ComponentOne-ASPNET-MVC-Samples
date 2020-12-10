using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Items",new OptionItem{Values = new List<string> {"1000", "10000", "100000", "200000", "500000"},CurrentValue = "100000"}},
                {"RenderEngine", new OptionItem {Values = new List<string> {"Svg", "WebGL"}, CurrentValue = "WebGL"}},
            }
        };
        public ActionResult Scatter(IFormCollection collection)
        {
            _options.LoadPostData(collection);
            ViewBag.DemoOptions = _options;
            Random ran = new Random();
            var model = Dot.GetData(ran.NextDouble() - 0.5, ran.NextDouble() - 0.5, Convert.ToInt32(_options.Options["items"].CurrentValue), 0.5);
            return View(model);
        }
    }
}