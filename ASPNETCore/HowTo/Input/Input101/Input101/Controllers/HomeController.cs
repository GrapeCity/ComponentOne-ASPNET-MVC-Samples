using Microsoft.AspNetCore.Mvc;
using Input101.Models;

namespace Input101.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new InputModel());
        }
    }
}
