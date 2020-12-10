using Microsoft.AspNetCore.Mvc;
using AccessibilityExtender.Models;

namespace AccessibilityExtender.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Sale.GetData(100));
        }
    }
}
