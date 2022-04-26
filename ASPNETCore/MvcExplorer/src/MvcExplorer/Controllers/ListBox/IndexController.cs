using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Case Sensitive Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}},
                {"Virtualization Threshold",new OptionItem{ Values = new List<string> { "Disable" , "0" }, CurrentValue = "Disable"}}
            }
        };
        public ActionResult Index(IFormCollection collection)
        {
            _options.LoadPostData(collection);
            ViewBag.DemoOptions = _options;
            ViewBag.Cities = Cities.GetCities();
            return View();
        }
    }
}
