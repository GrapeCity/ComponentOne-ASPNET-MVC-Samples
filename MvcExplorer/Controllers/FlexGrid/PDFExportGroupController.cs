using C1.Web.Mvc;
using C1.Web.Mvc.Grid;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;

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