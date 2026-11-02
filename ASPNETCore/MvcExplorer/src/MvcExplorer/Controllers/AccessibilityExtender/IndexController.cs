using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public class AccessibilityExtender : Controller
    {
        public ActionResult Index()
        {
            return View(Sale.GetData(100));
        }
    }
}
