using System.Collections.Generic;
using System.Web.Mvc;
using C1.Web.Mvc.Chart;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult ChartAnimation()
        {
            return View(CustomerOrder.GetCountryGroupOrderData());
        }
    }
}