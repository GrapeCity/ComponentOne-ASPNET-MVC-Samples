using FlexSheetExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult ExcelIO()
        {
            return View(SALES);
        }
    }
}
