using C1.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class ComboBoxController : Controller
    {
        private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Case Sensitive Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}},
                {"Handle Wheel",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "True"}},
                {"Virtualization Threshold",new OptionItem{ Values = new List<string> { "Disable" , "0" }, CurrentValue = "Disable"}},
                {"Key Action Print Characters", new OptionItem{ Values = new List<string> { InputKeyAction.OpenDropdown.ToString() , InputKeyAction.ChangeSelectionOnly.ToString() }, CurrentValue = InputKeyAction.OpenDropdown.ToString()}},
                {"Key Action Down Arrow", new OptionItem{ Values = new List<string> { InputKeyAction.OpenDropdown.ToString() , InputKeyAction.ChangeSelectionOnly.ToString() }, CurrentValue = InputKeyAction.OpenDropdown.ToString()}},
                {"Key Action Up Arrow", new OptionItem{ Values = new List<string> { InputKeyAction.OpenDropdown.ToString() , InputKeyAction.ChangeSelectionOnly.ToString() }, CurrentValue = InputKeyAction.OpenDropdown.ToString()}},
                {"Key Action Tab", new OptionItem{ Values = new List<string> { TabKeyAction.CloseAndKeepFocus.ToString() , TabKeyAction.CloseAndMoveFocus.ToString() }, CurrentValue = TabKeyAction.CloseAndKeepFocus.ToString()}}
            }
        };
        private readonly C1NWindEntities _db;

        public ComboBoxController(C1NWindEntities db)
        {
            _db = db;
        }

        public ActionResult Index(IFormCollection collection)
        {
            _options.LoadPostData(collection);
            ViewBag.DemoOptions = _options;
            ViewBag.Countries = Countries.GetCountries();
            ViewBag.Cities = Cities.GetCities();
            return View();
        }
    }
}
