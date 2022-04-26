using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        //
        // GET: /Grouping/

        public ActionResult Grouping()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupBy", new object[]{"ShipCountry", "ShipCity", "ShipCountry and ShipCity", "Freight", "ShippedDate","None"}}
                }
            };

            var nwind = new C1NWindEntities();
            return View(nwind.Orders);
        }

    }
}
