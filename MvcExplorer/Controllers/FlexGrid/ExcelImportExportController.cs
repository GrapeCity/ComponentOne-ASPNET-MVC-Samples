using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult ExcelImportExport()
        {
            return View(Sale.GetData(500));
        }
    }
}