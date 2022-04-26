using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _sortingDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            { 
                {"Sorting type", new OptionItem {Values = new List<string> {"None", "SingleColumn", "MultiColumn"}, CurrentValue = "SingleColumn"}}
            }
        };
        public ActionResult Sorting(FormCollection collection)
        {
            _sortingDataModel.LoadPostData(collection);                                                      
            ViewBag.DemoOptions = _sortingDataModel;
            return View(Sale.GetData(50));
        }
    }
}
