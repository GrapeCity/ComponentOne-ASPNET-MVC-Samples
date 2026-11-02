using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult CustomFunction()
        {
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
