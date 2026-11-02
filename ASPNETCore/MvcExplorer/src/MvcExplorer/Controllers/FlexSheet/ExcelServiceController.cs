using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Views.FlexSheet
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult ExcelService()
        {
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
