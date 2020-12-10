using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MvcExplorer.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _columnPinningDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Pinning Type", new OptionItem {Values = new List<string> {"None", "SingleColumn", "ColumnRange", "Both"}, CurrentValue = "SingleColumn"}}
            }
        };

        public ActionResult ColumnPinning(IFormCollection collection)
        {
            _columnPinningDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _columnPinningDataModel;
            return View();
        }

        public ActionResult ColumnPinning_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _columnPinningDataModel.LoadPostData(data);
            var model = Sale.GetData(500);
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}
