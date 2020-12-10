using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public class EnhancementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
