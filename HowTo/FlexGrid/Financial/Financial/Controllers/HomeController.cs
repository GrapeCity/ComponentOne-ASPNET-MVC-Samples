using Financial.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Financial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Intervals = new List<object> { 1000, 500, 100, 10, 1 };
            ViewBag.BatchSizes = new List<object> { 100, 50, 10, 5, 1 };
            return View(Company.GetData());
        }
    }
}