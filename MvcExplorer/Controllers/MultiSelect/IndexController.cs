using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectController : Controller
    {
        private readonly ControlOptions _multiSelectOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Show Filter Input", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Check On Filter", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Case Sensitive Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}}
            }
        };

        public ActionResult Index(FormCollection collection)
        {
            IValueProvider data = collection;
            _multiSelectOptions.LoadPostData(data);
            ViewBag.Countries = Countries.GetCountries();
            ViewBag.DemoOptions = _multiSelectOptions;
            return View();
        }
    }
}