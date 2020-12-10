using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Selection()
        {
            ChartOptions opts = new ChartOptions();

            ViewBag.typeIndex = 0;
            ViewBag.stackIndex = 0;
            ViewBag.selectionIndex = 1;
            return View(opts);
        }

        [HttpPost]
        public ActionResult Selection(ChartOptions opts)
        {
            ViewBag.typeIndex = ChartOptions.ChartTypes.IndexOf(opts.type);
            ViewBag.stackIndex = ChartOptions.Stackings.IndexOf(opts.stack);
            ViewBag.selectionIndex = ChartOptions.SelectionModes.IndexOf(opts.selectionmode);

            return View(opts);
        }
    }
}
