using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class CollectionViewController : Controller
    {
        private readonly ControlOptions _optionsModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Refresh On Edit", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult Index(FormCollection data)
        {
            _optionsModel.LoadPostData(data);
            ViewBag.DemoOptions = _optionsModel;
            var model = Sale.GetData(500);
            return View(model);
        }
    }
}