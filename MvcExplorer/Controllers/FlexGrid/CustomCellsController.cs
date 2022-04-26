using System.Web.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _customCellsDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {   
                {"Formatting", new OptionItem {Values = new List<string> {"On", "Off"}, CurrentValue = "Off"}},
                {"Css Class All", new OptionItem {Values = new List<string> {"None", "Red" , "Blue" , "Yellow"}, CurrentValue = "None"}}
            }
        };
        public ActionResult CustomCells(FormCollection collection)
        {
            _customCellsDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _customCellsDataModel;
            return View(Sale.GetData(500));
        }
    }
}
