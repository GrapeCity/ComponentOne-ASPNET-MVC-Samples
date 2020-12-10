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
        private readonly ControlOptions _preserveWhiteSpaceDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Preserve White Space", new OptionItem {Values = new List<string> { "True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult PreserveWhiteSpace(IFormCollection collection)
        {
            _preserveWhiteSpaceDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _preserveWhiteSpaceDataModel;
            return View();
        }

        public ActionResult PreserveWhiteSpace_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _columnPinningDataModel.LoadPostData(data);
            var model = Sale.GetData(50).ToList();
            model.ForEach(x => {
                if (x.Color.Equals("Black") || x.Color.Equals("White"))
                {
                    x.Color = string.Format("       {0} ", x.Color);
                }
            });
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}