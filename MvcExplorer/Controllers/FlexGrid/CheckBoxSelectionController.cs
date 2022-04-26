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
        // GET: /CheckboxSelection/

        public ActionResult CheckboxSelection()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"CheckboxColumn", new object[]{"Header", "OrderID", "ShipCountry", "ShipCity"}},
                    {"ShowCheckAll", new object[]{"True", "False"}}
                }
            };

            var nwind = new C1NWindEntities();
            return View(nwind.Orders.Take(100));
        }

    }
}
