﻿using C1.Web.Mvc;
using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;

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

        public ActionResult Filter(IFormCollection data)
        {
            _filterOptions.LoadPostData(data);
            ViewBag.DemoOptions = _filterOptions;
            ViewBag.FilterTypes = GetFilterTypes(_filterOptions);
            return View();
        }

        public ActionResult Filter_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
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
