﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MultiRowExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult Freezing()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateSettings()
            };
            return View();
        }

        public ActionResult Freezing_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"FrozenColumns", new object[]{1, 0, 2, 3}},
                {"FrozenRows", new object[]{2, 0, 1, 3, 4, 5}}
            };

            return settings;
        }
    }
}
