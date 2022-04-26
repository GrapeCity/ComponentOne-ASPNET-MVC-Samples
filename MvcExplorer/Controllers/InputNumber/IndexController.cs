using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

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

        public ActionResult Index(FormCollection collection)
        {
            IValueProvider data = collection;
            _optionModel.LoadPostData(data);
            ViewBag.DemoOptions = _optionModel;
            return View();
        }
    }
}
