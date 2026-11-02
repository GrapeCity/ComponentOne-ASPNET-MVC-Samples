using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class Input101Controller : Controller
    {
        public IActionResult Index()
        {
            return View(new InputModel());
        }
    }
}
