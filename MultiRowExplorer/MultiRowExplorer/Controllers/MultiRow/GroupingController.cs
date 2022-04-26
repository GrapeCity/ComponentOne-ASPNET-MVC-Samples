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
        //
        // GET: /Grouping/

        public ActionResult Grouping()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupBy", new object[]{"Customer State", "Customer City", "Customer State and Customer City", "Ordered Date", "Shipped Date", "Amount","None"}},
                    { "ShowGroups", new object[] {true, false } }
                }
            };

            return View(Orders.GetOrders());
        }
    }
}