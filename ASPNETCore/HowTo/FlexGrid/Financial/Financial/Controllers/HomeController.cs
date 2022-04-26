using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Financial.Models;

namespace Financial.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Intervals = new List<object> { 1000, 500, 100, 10, 1 };
            ViewBag.BatchSizes = new List<object> { 100, 50, 10, 5, 1 };
            return View(Company.GetData());
        }
    }
}
