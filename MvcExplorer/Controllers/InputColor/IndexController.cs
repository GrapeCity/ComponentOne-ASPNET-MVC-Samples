using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class InputColorController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
          Options = new OptionDictionary
                {
                    {"Show Color String", new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}}
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
