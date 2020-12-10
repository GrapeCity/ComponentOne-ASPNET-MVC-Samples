using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class TreeMapController : Controller
    {
        // GET: MaxDepth
        public ActionResult MaxDepth()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateMaxDepthSettings(),
                DefaultValues = GetMaxDepthDefaultValues()
            };
            return View(ThingSale.GetDate());
        }

        private static IDictionary<string, object[]> CreateMaxDepthSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"MaxDepth", new object[]{ 0, 1, 2, 3, 4}}
            };

            return settings;
        }

        private static IDictionary<string, object> GetMaxDepthDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"MaxDepth", 2}
            };

            return defaultValues;
        }
    }
}