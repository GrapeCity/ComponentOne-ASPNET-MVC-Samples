using C1.Web.Mvc.Grid;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                {"Sorting By", new OptionItem {Values = new List<string> { "ID", "Start", "End", "Country", "Product", "Color", "Amount", "Amount2", "Discount", "Active" }, CurrentValue = "ID"}},
                {"Sorting type", new OptionItem {Values = new List<string> {"None", "SingleColumn", "MultiColumn"}, CurrentValue = "SingleColumn"}},
                { "Sorting Order", new OptionItem
                {
                    Values = Enum.GetNames(typeof(C1.Web.Mvc.Grid.SortOrder)).ToList(),
                    CurrentValue = C1.Web.Mvc.Grid.SortOrder.AscDesc.ToString()
                }
                }
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
