using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult MultipleHeaders()
        {
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
