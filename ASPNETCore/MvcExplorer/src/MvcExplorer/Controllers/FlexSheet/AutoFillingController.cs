using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult AutoFilling()
        {
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
