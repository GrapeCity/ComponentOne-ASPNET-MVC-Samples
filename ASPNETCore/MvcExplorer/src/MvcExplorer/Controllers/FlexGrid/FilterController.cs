using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IEnumerable<Sale> _filterDataModel = Sale.GetData(500).ToList();

        private static OptionItem CreateOptionItem()
        {
            return new OptionItem { Values = new List<string> { "None", "Condition", "Value", "Both" }, CurrentValue = "Both" };
        }

        private readonly ControlOptions _gridFilterModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Country", CreateOptionItem()},
                {"Product", CreateOptionItem()},
                {"Color", CreateOptionItem()},
                {"Amount", CreateOptionItem()},
                {"Active", CreateOptionItem()}
            }
        };

        public ActionResult Filter(IFormCollection data)
        {
            _gridFilterModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridFilterModel;
            ViewBag.FilterTypes = GetFilterTypes(_gridFilterModel);
            return View();
        }

        public ActionResult Filter_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _filterDataModel));
        }

        private Dictionary<string, FilterType> GetFilterTypes(ControlOptions controlOptions)
        {
            var filterTypes = new Dictionary<string, FilterType>();
            foreach (var item in controlOptions.Options)
            {
                filterTypes.Add(item.Key, (FilterType)Enum.Parse(typeof(FilterType), item.Value.CurrentValue));
            }
            return filterTypes;
        }
    }
}
