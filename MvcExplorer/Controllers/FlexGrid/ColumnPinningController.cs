using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _columnPinningDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {            
                {"Pinning Type", new OptionItem {Values = new List<string> { "None", "SingleColumn", "ColumnRange", "Both"}, CurrentValue = "SingleColumn"}}
            }
        };

        public ActionResult ColumnPinning(FormCollection collection)
        {
            IValueProvider data = collection;
            _columnPinningDataModel.LoadPostData(data);
            var model = Sale.GetData(500);
            ViewBag.DemoOptions = _columnPinningDataModel;
            return View(model);
        }
    }
}