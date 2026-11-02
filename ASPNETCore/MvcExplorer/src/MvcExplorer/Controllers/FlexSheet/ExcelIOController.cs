using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult ExcelIO()
        {
            ViewBag.DemoDescription = false;
            return View(SALES);
        }
    }
}
