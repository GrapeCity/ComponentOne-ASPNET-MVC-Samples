using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TransposedMultiRowExplorer.Models;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

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

        public ActionResult Index(IFormCollection collection)
        {
            _options.LoadPostData(collection);
            ViewBag.DemoOptions = _options;
            return View(Orders.GetOrders());
        }
    }
}