using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Annotation()
        {
            ViewBag.BasicData = BasicSale.GetBasicSales();
            ViewBag.FbData = JsonDataReader.GetFromFile<FinanceData>("fb.json");
            return View();
        }
    }
}
