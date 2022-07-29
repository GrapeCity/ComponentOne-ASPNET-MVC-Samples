using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        // GET: CalculationPrecision
        public ActionResult CalculationPrecision(int val = 14)
        {
            int CalculationPrecision = val;
            return View(CalculationPrecision);
        }

        [HttpPost]
        public ActionResult SetCalculationPrecision(int numb)
        {
            return RedirectToAction("CalculationPrecision", "FlexSheet", new { val = numb});
        }
    }
}