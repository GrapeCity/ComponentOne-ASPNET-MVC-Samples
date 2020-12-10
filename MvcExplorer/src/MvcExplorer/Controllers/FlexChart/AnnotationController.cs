using System;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Annotation()
        {
            ViewBag.BasicData = BasicSale.GetBasicSales();
            ViewBag.FbData = JsonDataReader.GetFromAssembly<FinanceData>("fb.json");
            return View();
        }
    }
}
