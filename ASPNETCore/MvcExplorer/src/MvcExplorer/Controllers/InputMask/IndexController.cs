using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class InputMaskController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Overwrite Mode",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult Index(IFormCollection collection)
        {
            _optionModel.LoadPostData(collection);
            ViewBag.DemoOptions = _optionModel;
            return View();
        }
    }
}
