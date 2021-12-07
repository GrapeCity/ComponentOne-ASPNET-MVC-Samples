using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class InputColorController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Show Color String",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}}
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
