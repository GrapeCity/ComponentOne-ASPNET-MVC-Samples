using Microsoft.AspNetCore.Mvc;
using FilterPanel.Models;
using System.Linq;

namespace FilterPanel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(Sale.GetData(100).ToList());
        }
    }
}
