using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class DashboardLayoutController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            var data = new ClientSettingsModel
            {
                Settings = CreateLayoutSettings(),
            };
            data.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = data;
            return View(new DashboardData());
        }

        private static IDictionary<string, object[]> CreateLayoutSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AllowDrag", new object[]{true, false}},
                {"AllowResize", new object[]{true, false}}
            };

            return settings;
        }
    }
}