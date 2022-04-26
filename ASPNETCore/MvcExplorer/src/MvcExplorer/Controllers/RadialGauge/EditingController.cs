using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class RadialGaugeController : Controller
    {
        public ActionResult Editing()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateEditingSettings()
            };
            return View();
        }

        private static IDictionary<string, object[]> CreateEditingSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"IsReadOnly", new object[]{false, true }},
                {"Step", new object[]{0.5, 1, 2}},
                {"ShowTicks", new object[]{ false, true}},
                {"ShowTickText", new object[]{ false, true}},
                {"NeedleShape", new object[]{ "None", "Triangle", "Diamond", "Hexagon", "Rectangle", "Arrow", "WideArrow", "Pointer", "WidePointer", "Outer"}},
                {"NeedleLength", new object[]{ "Outer", "Middle", "Inner"}},
                {"HandleWheel", new object[]{true, false }}
            };

            return settings;
        }
    }
}
