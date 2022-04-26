using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectListBoxController : Controller
    {
        private C1NWindEntities db = new C1NWindEntities();

        private readonly ControlOptions _multiSelectOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Show Select All Checkbox", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Show Filter Input", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Case Sensitive Search", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "false"}},
                {"Delay Filter", new OptionItem {Values = new List<string> {"500", "1000", "2000"}, CurrentValue = "500"}},
                {"Virtualization Threshold",new OptionItem{ Values = new List<string> { "Disable" , "0" }, CurrentValue = "Disable"}}
            }
        };

        public ActionResult Index(FormCollection collection)
        {
            _multiSelectOptions.LoadPostData(collection);
            ViewBag.DemoOptions = _multiSelectOptions;
            return View(db.Products);
        }
    }
}
