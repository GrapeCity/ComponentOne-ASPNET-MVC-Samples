using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: DetachedPanels
        public ActionResult DetachedPanels(FormCollection collection)
        {
            return View();
        }
    }
}