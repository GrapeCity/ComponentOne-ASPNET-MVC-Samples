using System.Web.Mvc;
using System.Collections.Generic;
using MultiRowExplorer.Models;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult Styling()
        {
            return View(Orders.GetOrders());
        }
    }
}