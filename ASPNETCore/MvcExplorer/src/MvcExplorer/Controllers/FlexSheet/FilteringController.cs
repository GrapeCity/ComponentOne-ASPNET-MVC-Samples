using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult Filtering()
        {
            ViewBag.DemoDescription = false;
            return View(SALES);
        }
    }
}
