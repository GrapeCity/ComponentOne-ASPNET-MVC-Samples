using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
