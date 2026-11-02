using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}