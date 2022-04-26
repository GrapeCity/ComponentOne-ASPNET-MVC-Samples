using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridSearchOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Delay",new OptionItem{Values = new List<string> {"100", "300", "500", "800", "1000"}, CurrentValue = "500"}},
                {"Css Match", new OptionItem {Values = new List<string> {"Default", "color-match", "underline-match", "style-match"}, CurrentValue = "Default"}},
                {"Case Sensitive Search", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Search All Columns", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult Searching(FormCollection data)
        {
            _gridSearchOptions.LoadPostData(data);
            ViewBag.DemoOptions = _gridSearchOptions;
            return View(Sale.GetData(200));
        }
    }
}
