using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MultiRowExplorer.Models;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
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
            return View();
        }

        public ActionResult Index_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            var model = Orders.GetOrders();
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}