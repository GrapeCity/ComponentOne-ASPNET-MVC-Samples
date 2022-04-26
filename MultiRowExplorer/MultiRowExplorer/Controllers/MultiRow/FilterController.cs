using C1.Web.Mvc;
using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private static OptionItem CreateOptionItem()
        {
            return new OptionItem { Values = new List<string> { "None", "Condition", "Value", "Both" }, CurrentValue = "Both" };
        }

        private readonly ControlOptions _filterOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"CustomerState", CreateOptionItem()},
                {"CustomerCity", CreateOptionItem()},
                {"ShipperName", CreateOptionItem()},
                {"ShipperExpress", CreateOptionItem()},
                {"Amount", CreateOptionItem()}
            }
        };

        public ActionResult Filter(FormCollection data)
        {
            _filterOptions.LoadPostData(data);
            ViewBag.DemoOptions = _filterOptions;
            ViewBag.FilterTypes = GetFilterTypes(_filterOptions);
            return View(Orders.GetOrders());
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
