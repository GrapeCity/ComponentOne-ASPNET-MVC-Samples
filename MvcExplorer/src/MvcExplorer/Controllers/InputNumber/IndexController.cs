using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class InputNumberController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Handle Wheel",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "True"}}
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
