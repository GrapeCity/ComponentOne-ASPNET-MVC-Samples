using Microsoft.AspNetCore.Mvc;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult ExcelIO()
        {
            return View(SALES);
        }
    }
}
