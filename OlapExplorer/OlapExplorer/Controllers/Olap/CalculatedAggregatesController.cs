using OlapExplorer.Models;
using System.Collections;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using C1.Web.Mvc.Olap;

namespace OlapExplorer.Controllers.Olap
{
    public partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult CalculatedAggregates()
        {
            return View(ProductData.GetData(500));
        }
    }
}