using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

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
