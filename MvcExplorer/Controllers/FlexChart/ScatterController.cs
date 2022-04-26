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
        private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Items",new OptionItem{Values = new List<string> {"1000", "10000", "100000", "200000", "500000"},CurrentValue = "100000"}},
                {"RenderEngine", new OptionItem {Values = new List<string> {"Svg", "WebGL"}, CurrentValue = "WebGL"}},
            }
        };

        public ActionResult Scatter(FormCollection collection)
        {     
            IValueProvider data = collection;
            _options.LoadPostData(data);                                                              
            ViewBag.DemoOptions = _options;
            Random ran = new Random();
            var model = Dot.GetData(ran.NextDouble() - 0.5, ran.NextDouble() - 0.5, Convert.ToInt32(_options.Options["items"].CurrentValue), 0.5);
            return View(model);
        }
    }
}
