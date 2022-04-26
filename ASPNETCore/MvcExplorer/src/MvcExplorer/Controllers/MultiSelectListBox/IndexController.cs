using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectListBoxController : Controller
    {
        private readonly C1NWindEntities _db;

        public MultiSelectListBoxController(C1NWindEntities db)
        {
            _db = db;
        }

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

        public ActionResult Index(IFormCollection collection)
        {
          _multiSelectOptions.LoadPostData(collection);
          ViewBag.DemoOptions = _multiSelectOptions;
          return View(_db.Products);
        }
  }
}
