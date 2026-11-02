using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult Sorting()
        {
            ViewBag.DemoDescription = false;
            return View(SALES);
        }
    }
}
