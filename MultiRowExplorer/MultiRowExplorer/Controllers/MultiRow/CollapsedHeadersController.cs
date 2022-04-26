using System.Web.Mvc;
using System.Collections.Generic;
using MultiRowExplorer.Models;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult CollapsedHeaders()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    { "CollapsedHeaders", new object[] {"False", "True", "null"} },
                    { "ShowHeaderCollapseButton", new object[] { false, true} }
                },
                DefaultValues=new Dictionary<string, object>
                {
                    { "CollapsedHeaders", true },
                    { "ShowHeaderCollapseButton", true }
                }
            };
            return View(Orders.GetOrders());
        }
    }
}