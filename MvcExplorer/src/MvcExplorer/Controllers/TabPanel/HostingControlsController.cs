using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: HostingControls
        public ActionResult HostingControls(IFormCollection collection)
        {
            ViewBag.GridData = Sale.GetData(50);
            ViewBag.ChartData = Fruit.GetFruitsSales();
            return View();
        }
    }
}