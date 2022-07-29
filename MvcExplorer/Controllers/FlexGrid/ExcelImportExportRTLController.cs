using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        // GET: ExcelImportExportDirection
        public ActionResult ExcelImportExportRTL()
        {
            return View(Sale.GetData(500));
        }
    }
}