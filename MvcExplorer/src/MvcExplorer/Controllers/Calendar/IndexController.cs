using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class CalendarController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"FirstDayOfWeek", Enum.GetValues(typeof(DayOfWeek)).Cast<object>().ToArray()},
                {"ShowHeader", new object[]{true, false}},
                {"MonthView", new object[]{true, false}},
                {"RepeatButtons", new object[]{true, false}},
                {"ShowYearPicker", new object[]{true, false}},
                {"HandleWheel", new object[]{true, false}}
            };

            return settings;
        }
    }
}
