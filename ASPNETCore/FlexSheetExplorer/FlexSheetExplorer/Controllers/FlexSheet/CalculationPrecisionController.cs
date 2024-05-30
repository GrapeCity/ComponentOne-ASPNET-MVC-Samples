using Microsoft.AspNetCore.Mvc;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult CalculationPrecision(int val = 14)
        {
            int CalculationPrecision = val;
            return View(CalculationPrecision);
        }

    }
}
