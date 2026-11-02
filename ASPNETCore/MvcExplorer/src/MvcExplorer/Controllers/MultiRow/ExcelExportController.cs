using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult ExcelExport()
        {
            return View(Sale.GetData(100));
        }
    }
}