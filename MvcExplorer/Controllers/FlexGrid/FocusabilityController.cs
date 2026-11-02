using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _focusabilityDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {            
                {"Headers Focusability", new OptionItem {Values = new List<string> { "None", "Column", "Row", "All"}, CurrentValue = "Column"}}
            }
        };

        public ActionResult Focusability(FormCollection collection)
        {
            IValueProvider data = collection;
            _focusabilityDataModel.LoadPostData(data);
            var model = Sale.GetData(500);
            ViewBag.DemoOptions = _focusabilityDataModel;
            return View(model);
        }
    }
}