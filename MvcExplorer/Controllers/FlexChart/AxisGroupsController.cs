using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult AxisGroups()
        {
            var model = new ClientSettingsModel
            {
                Settings = CreateGroupsOptionSettings(),
                DefaultValues = new Dictionary<string, object> { 
                    {"AxisX.GroupsOptions.Display", "Show"}
                }
            };

            ViewBag.CitiesSales = CitySale.GetData(8);

            return View(model);
        }

        private static IDictionary<string, object[]> CreateGroupsOptionSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AxisX.GroupsOptions.Display", new object[]{"None", "Show", "ShowGrid"}}
            };

            return settings;
        }
    }
}
