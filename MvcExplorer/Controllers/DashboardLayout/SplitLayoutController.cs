using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class DashboardLayoutController
    {
        // GET: Index
        public ActionResult SplitLayout()
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