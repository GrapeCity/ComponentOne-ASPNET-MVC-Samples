using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace MvcExplorer.Controllers
{
    public partial class DashboardLayoutController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
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
