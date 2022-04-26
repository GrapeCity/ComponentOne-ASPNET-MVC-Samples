using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc.Chart;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult PanningAndScaling()
        {
            return View(JsonDataReader.GetFromFile<FinanceData>("fb2.json"));
        }
    }
}
