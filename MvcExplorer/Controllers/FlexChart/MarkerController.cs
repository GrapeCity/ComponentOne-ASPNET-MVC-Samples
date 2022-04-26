using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Marker()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"Lines", new object[]{"None", "Vertical", "Horizontal", "Both"}},
                    {"Alignment", new object[]{"Auto", "Right", "Left", "Bottom", "Top", "Left & Bottom", "Left & Top"}},
                    {"Interaction", new object[]{"None", "Move", "Drag"}}
                },
                DefaultValues = new Dictionary<string, object>
                {
                    {"Lines", "Vertical"},
                    {"Interaction", "Move"}
                }
            };
            
            return View(_apple.Sales);
        }
    }
}
