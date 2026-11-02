using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexViewerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
