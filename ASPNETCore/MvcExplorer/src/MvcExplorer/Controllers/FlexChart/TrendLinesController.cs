using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult TrendLines()
        {
            var model = Sale.GetData(50);
            return View(model);
        }
    }
}
