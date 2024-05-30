using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
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
