using ExcelBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcelBook.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExcelLike()
        {
            ViewBag.FontList = FontName.GetFontNameList();
            ViewBag.FontSizeList = FontSize.GetFontSizeList();
            ViewBag.ChartTypes = new[]
            {"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"};
            return View(Sale.GetData(100));
        }

        public ActionResult StandAlone()
        {
            return View(Sale.GetData(100));
        }
    }
}