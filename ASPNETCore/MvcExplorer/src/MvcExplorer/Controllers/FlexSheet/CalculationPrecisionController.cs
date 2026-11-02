using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult CalculationPrecision(int val = 14)
        {
            ViewBag.DemoDescription = false;
            int CalculationPrecision = val;
            return View(CalculationPrecision);
        }

    }
}
