using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class TreeMapController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateIndexSettings()
            };
            return View(FoodSale.GetDate());
        }

        private static IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Type", new object[]{ "Squarified", "Horizontal", "Vertical"}}
            };

            return settings;
        }
    }
}