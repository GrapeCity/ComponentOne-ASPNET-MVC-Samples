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

        [HttpPost]
        public ActionResult SetCalculationPrecision([FromBody] int numb)
        {
            return RedirectToAction("CalculationPrecision", "FlexSheet", new { val = numb });
        }
    }
}
