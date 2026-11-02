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
        private readonly ControlOptions _focusabilityDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Headers Focusability", new OptionItem {Values = new List<string> { "None", "Column", "Row", "All"}, CurrentValue = "Column"}}
            }
        };

        public ActionResult Focusability(IFormCollection collection)
        {
            _focusabilityDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _focusabilityDataModel;
            return View();
        }

        public ActionResult Focusability_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _focusabilityDataModel.LoadPostData(data);
            var model = Sale.GetData(500);
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}
