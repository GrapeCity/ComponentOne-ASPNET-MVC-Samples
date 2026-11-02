using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult CellMerging()
        {
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
