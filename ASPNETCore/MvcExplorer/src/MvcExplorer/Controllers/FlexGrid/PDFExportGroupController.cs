using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IEnumerable<Sale> pdfExportGroupData = Sale.GetData(500);
        public ActionResult PDFExportGroup()
        {
            ViewBag.GroupedFlexGridData = pdfExportGroupData;
            return View();
        }
    }
}