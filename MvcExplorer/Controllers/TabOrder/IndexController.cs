using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class TabOrderController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Tab Order",new OptionItem{ Values = new List<string> { "Default", "Customized"}, CurrentValue = "Default"}}
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