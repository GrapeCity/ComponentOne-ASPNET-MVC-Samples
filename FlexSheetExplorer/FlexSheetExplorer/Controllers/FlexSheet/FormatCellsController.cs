using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexSheetExplorer.Models;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult FormatCells()
        {
            ViewBag.FontList = FontName.GetFontNameList();
            ViewBag.FontSizeList = FontSize.GetFontSizeList();
            return View();
        }
    }
}
