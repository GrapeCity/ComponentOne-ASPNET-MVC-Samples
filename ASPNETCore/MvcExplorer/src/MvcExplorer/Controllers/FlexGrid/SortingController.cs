using C1.Web.Mvc;
using C1.Web.Mvc.Grid;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    Values = Enum.GetNames(typeof(SortOrder)).ToList(),
                    CurrentValue = SortOrder.AscDesc.ToString()
                }
    }
            }
        };
        public ActionResult Sorting(IFormCollection collection)
        {
            _sortingDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _sortingDataModel;
            return View();
        }

        public ActionResult Sorting_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}
