using C1.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class InputDateController : Controller
    {
         private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Key Action Down Arrow", new OptionItem{ Values = new List<string> { InputKeyAction.OpenDropdown.ToString() , InputKeyAction.ChangeSelectionOnly.ToString() }, CurrentValue = InputKeyAction.OpenDropdown.ToString()}},
                {"Key Action Up Arrow", new OptionItem{ Values = new List<string> { InputKeyAction.OpenDropdown.ToString() , InputKeyAction.ChangeSelectionOnly.ToString() }, CurrentValue = InputKeyAction.OpenDropdown.ToString()}},
                {"Key Action Tab", new OptionItem{ Values = new List<string> { TabKeyAction.CloseAndKeepFocus.ToString() , TabKeyAction.CloseAndMoveFocus.ToString() }, CurrentValue = TabKeyAction.CloseAndKeepFocus.ToString()}},
                {"Show Confirmation Buttons",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}},
            }
        };

        public ActionResult Index(IFormCollection collection)
        {
            _options.LoadPostData(collection);
            ViewBag.DemoOptions = _options;
            return View();
        }

    }
}
