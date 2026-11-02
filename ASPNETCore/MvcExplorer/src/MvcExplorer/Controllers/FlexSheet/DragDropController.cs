using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult DragDrop()
        {
            ViewBag.DemoDescription = false;
            return View();
        }

        public ActionResult DragDropFrame()
        {
            return View();
        }
    }
}
