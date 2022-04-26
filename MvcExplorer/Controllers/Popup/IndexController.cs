using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
