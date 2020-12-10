﻿using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcExplorer.Controllers
{
    partial class DashboardLayoutController : Controller
    {
        // GET: /<controller>/
        public IActionResult AutoGridLayout()
        {
            var data = new ClientSettingsModel
            {
                Settings = CreateAutoGridLayoutSettings(),
                DefaultValues = new Dictionary<string, object> {
                    {"Layout.CellSpacing", 6},
                    {"Layout.CellSize", 303},
                    {"Layout.GroupSpacing", 10}
                }
            };
            data.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = data;
            return View(new DashboardData());
        }

        private static IDictionary<string, object[]> CreateAutoGridLayoutSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AllowDrag", new object[]{true, false}},
                {"AllowResize", new object[]{true, false}},
                {"Layout.Orientation", new object[]{ "Vertical", "Horizontal"}},
                {"Layout.MaxRowsOrColumns", new object[]{ 3, 4, 5, 6 }},
                {"Layout.CellSize", new object[]{160, 200, 303, 400}},
                {"Layout.CellSpacing", new object[]{ 0, 3, 6, 9}},
                {"Layout.GroupSpacing", new object[]{ 0, 5, 10, 15}}
            };

            return settings;
        }
    }
}

