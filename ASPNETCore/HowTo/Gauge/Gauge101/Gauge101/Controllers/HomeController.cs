using Microsoft.AspNetCore.Mvc;
using Gauge101.Models;

namespace Gauge101.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new GaugeModel());
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
