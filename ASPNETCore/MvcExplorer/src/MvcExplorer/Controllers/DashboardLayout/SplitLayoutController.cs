using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcExplorer.Controllers
{
    partial class DashboardLayoutController : Controller
    {
        // GET: /<controller>/
        public IActionResult SplitLayout()
        {
            var data = new ClientSettingsModel
            {
                Settings = CreateSplitLayoutSettings()
            };
            data.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = data;
            return View(new DashboardData());
        }

        private static IDictionary<string, object[]> CreateSplitLayoutSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AllowDrag", new object[]{true, false}},
                {"AllowResize", new object[]{true, false}},
                {"Layout.Orientation", new object[]{ "Vertical", "Horizontal"}}
            };

            return settings;
        }
    }
}
