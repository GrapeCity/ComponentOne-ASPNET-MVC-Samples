using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

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
