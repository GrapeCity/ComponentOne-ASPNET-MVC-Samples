using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult Selection()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateSelectionSettings()
            };

            return View(CustomerOrder.GetCountryGroupOrderData());
        }

        private static IDictionary<string, object[]> CreateSelectionSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"SelectedItemPosition", new object[]{Position.Top, Position.Bottom, Position.Left, Position.None, Position.Right, Position.Auto}},
                {"SelectedItemOffset", new object[]{0, 0.1, 0.2}},
                {"IsAnimated", new object[]{true, false}}
            };

            return settings;
        }
    }
}