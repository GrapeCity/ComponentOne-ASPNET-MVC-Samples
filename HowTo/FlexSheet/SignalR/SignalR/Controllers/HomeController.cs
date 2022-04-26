using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR.Models;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.FontList = FontName.GetFontNameList();
            ViewBag.FontSizeList = FontSize.GetFontSizeList();
            return View(WorkbookOM.WORKBOOK);
        }
    }
}