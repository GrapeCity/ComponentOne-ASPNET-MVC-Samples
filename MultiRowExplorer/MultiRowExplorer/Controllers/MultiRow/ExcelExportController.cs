using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult ExcelExport()
        {
            return View(Sale.GetData(100));
        }
    }
}