using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IEnumerable<Sale> _filterDataModel = Sale.GetData(500).ToList();

        private static OptionItem CreateOptionItem()
        {
            return new OptionItem { Values = new List<string> { "None", "Condition", "Value", "Both" }, CurrentValue = "Both" };
        }

        private static OptionItem CreateOptionItemForFilterModes()
        {
            return new OptionItem { Values = new List<string> { "SingleFilter", "MultiFilter" }, CurrentValue = "SingleFilter" };
        }

        private readonly ControlOptions _gridFilterModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Country", CreateOptionItem()},
                {"Product", CreateOptionItem()},
                {"Color", CreateOptionItem()},
                {"Amount", CreateOptionItem()},
                {"Active", CreateOptionItem()},
                {"DefaultFilterMode", CreateOptionItemForFilterModes() }
            }
        };

        public ActionResult Filter(IFormCollection data)
        {
            _gridFilterModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridFilterModel;
            ViewBag.FilterTypes = GetFilterTypes(_gridFilterModel);
            ViewBag.FilterMode = GetFilterMode(_gridFilterModel);
            return View();
        }

        public ActionResult Filter_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _filterDataModel));
        }

        private FilterMode GetFilterMode(ControlOptions controlOptions)
        {
            var defaultFilterMode = controlOptions.Options["DefaultFilterMode"].CurrentValue;
            return (FilterMode)Enum.Parse(typeof(FilterMode), defaultFilterMode);
        }

        private Dictionary<string, FilterType> GetFilterTypes(ControlOptions controlOptions)
        {
            var filterTypes = new Dictionary<string, FilterType>();
            foreach (var item in controlOptions.Options)
            {
                if (string.Equals(item.Key, "DefaultFilterMode", StringComparison.Ordinal))
                    continue;

                filterTypes.Add(item.Key, (FilterType)Enum.Parse(typeof(FilterType), item.Value.CurrentValue));
            }
            return filterTypes;
        }
    }
}
