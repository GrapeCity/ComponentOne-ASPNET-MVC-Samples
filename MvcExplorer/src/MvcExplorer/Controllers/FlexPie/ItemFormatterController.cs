using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult ItemFormatter()
        {
            return View(CustomerOrder.GetCountryGroupOrderData());
        }
    }
}