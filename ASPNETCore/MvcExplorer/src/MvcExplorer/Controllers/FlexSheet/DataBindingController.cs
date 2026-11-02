using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public static List<Sale> SALES = CustomerSale.GetData(50).ToList();

        public ActionResult DataBinding()
        {
            ViewBag.DemoDescription = false;
            return View(SALES);
        }
    }
}
