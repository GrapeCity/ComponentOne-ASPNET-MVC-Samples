using System.Collections.Generic;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult FrozenCells()
        {
            ViewBag.DemoSettings = true;
            return View(Sale.GetData(500));
        }

        
    }
}
