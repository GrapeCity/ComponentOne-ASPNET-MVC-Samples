﻿using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class LinearGaugeController : Controller
    {
        public ActionResult Editing()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateEditingSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateEditingSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"IsReadOnly", new object[]{false, true }},
                {"Step", new object[]{0.5, 1, 2}},
                {"ShowTicks", new object[]{ false, true}},
                {"ShowTickText", new object[]{ false, true}},
                {"HandleWheel", new object[]{true, false }},
            };

            return settings;
        }
    }
}
