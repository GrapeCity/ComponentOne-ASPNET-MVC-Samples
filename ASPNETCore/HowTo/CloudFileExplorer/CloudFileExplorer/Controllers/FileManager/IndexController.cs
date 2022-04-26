using Microsoft.AspNetCore.Mvc;

namespace CloudFileExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}