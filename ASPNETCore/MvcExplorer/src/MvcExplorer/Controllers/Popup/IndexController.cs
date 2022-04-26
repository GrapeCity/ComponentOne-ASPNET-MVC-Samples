using Microsoft.AspNetCore.Mvc;
using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using C1.Web.Mvc;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class PopupController : Controller
    {
        public ActionResult Index()
        {
            var settings = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"ShowTrigger", Enum.GetValues(typeof(PopupTrigger)).Cast<object>().ToArray()},
                    {"HideTrigger", Enum.GetValues(typeof(PopupTrigger)).Cast<object>().ToArray()}
                }
            };
            settings.LoadRequestData(Request);
            ViewBag.DemoSettingsModel = settings;
            return View();
        }
    }
}
