using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: HostingControls
        public ActionResult HostingControls(FormCollection collection)
        {
            ViewBag.GridData = Sale.GetData(50);
            ViewBag.ChartData = Fruit.GetFruitsSales();
            return View();
        }
    }
}