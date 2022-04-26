using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

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

        public ActionResult Index(IFormCollection collection)
        {
            _multiSelectOptions.LoadPostData(collection);
            ViewBag.Countries = Countries.GetCountries();
            ViewBag.DemoOptions = _multiSelectOptions;
            return View();
        }
    }
}
