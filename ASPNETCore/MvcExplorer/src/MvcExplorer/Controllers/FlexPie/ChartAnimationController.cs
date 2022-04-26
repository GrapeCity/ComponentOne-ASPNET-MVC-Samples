using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController
    {
        public ActionResult ChartAnimation()
        {
            return View(CustomerOrder.GetCountryGroupOrderData());
        }
    }
}