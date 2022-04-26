using System.Web.Mvc;
using System.Collections.Generic;
using C1.Web.Mvc.Grid;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult CustomFooters()
        {
            return View(Sale.GetData(500));
        }
    }
}
