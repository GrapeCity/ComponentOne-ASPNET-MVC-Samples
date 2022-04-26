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
        public ActionResult ServerLoad(string loadType)
        {
            ViewBag.loadTypes = new string[] { "Xlsx", "Workbook" };
            object model;

            if (loadType == "Workbook")
            {
                model = WorkbookOM.GetWorkbook();
            }
            else
            {
                loadType = "Xlsx";
                model = "~/Content/xlsxFile/example1.xlsx";
            }
            ViewBag.LoadType = loadType;

            return View(model);
        }
    }
}
