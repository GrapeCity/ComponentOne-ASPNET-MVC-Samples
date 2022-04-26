using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: Index
        public ActionResult Index(FormCollection collection)
        {
            return View();
        }
    }
}