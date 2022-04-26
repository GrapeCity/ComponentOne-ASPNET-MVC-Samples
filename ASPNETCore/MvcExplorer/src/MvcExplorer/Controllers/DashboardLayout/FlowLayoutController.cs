using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcExplorer.Controllers
{
    partial class DashboardLayoutController : Controller
    {
        // GET: /<controller>/
        public IActionResult FlowLayout()
        {
            var data = new ClientSettingsModel
            {
                Settings = CreateFlowLayoutSettings(),
            };
            data.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = data;
            return View(new DashboardData());
        }

        private static IDictionary<string, object[]> CreateFlowLayoutSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AllowDrag", new object[]{true, false}},
                {"AllowResize", new object[]{true, false}},
                {"Layout.Direction", new object[]{"LeftToRight", "TopToDown", "RightToLeft", "BottomToUp"}}
            };

            return settings;
        }
    }
}

