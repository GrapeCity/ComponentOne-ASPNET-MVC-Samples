using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        // GET: GroupHeaders
        public ActionResult GroupHeaders()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    { "GroupBy", new object[] { "Country", "Color", "Country and Color", "None" } },
                    { "ShowGroups", new object[] {true, false } },
                    { "MultiRowGroupHeaders", new object[] {true, false } }
                }
            };

            return View(Sale.GetData(100));
        }
    }
}