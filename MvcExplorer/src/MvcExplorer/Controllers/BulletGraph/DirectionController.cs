﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class BulletGraphController : Controller
    {
        public ActionResult Direction()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateDirectionSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateDirectionSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Direction", new object[]{"Left", "Right", "Down", "Up"}}
            };

            return settings;
        }
    }
}
