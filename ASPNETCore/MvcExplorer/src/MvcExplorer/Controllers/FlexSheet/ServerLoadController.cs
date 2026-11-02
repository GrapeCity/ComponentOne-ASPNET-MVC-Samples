using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult ServerLoad()
        {
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
