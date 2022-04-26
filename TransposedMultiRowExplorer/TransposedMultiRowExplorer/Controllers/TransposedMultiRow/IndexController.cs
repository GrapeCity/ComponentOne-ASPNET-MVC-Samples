using System.Web.Mvc;
using System.Collections.Generic;
using TransposedMultiRowExplorer.Models;

namespace TransposedMultiRowExplorer.Controllers
{
    public partial class TransposedMultiRowController : Controller
    {
        private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Layout Definition",new OptionItem{Values = new List<string> {"Traditional", "Compact", "Detailed"},CurrentValue = "Compact"}}
            }
        };

        public ActionResult Index(FormCollection collection)
        {
          _options.LoadPostData(collection);
            var model = Orders.GetOrders();
            ViewBag.DemoOptions = _options;
            return View(model);
        }
    }
}