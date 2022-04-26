using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
