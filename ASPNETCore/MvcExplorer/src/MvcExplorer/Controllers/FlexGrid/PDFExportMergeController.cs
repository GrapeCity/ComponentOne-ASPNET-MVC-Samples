using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IEnumerable<Sale> pdfExportMergeData = Sale.GetData(500);
        public ActionResult PDFExportMerge()
        {
            ViewBag.MergedFlexGridData = pdfExportMergeData;
            return View();
        }
    }
}