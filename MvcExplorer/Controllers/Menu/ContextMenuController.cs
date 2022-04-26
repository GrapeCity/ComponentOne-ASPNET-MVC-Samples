using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class MenuController : Controller
    {
        public ActionResult ContextMenu()
        {
            return View();
        }
    }
}
