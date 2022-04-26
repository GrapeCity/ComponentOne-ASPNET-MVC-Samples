using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class PopupController : Controller
    {
        public ActionResult PopupDialog()
        {
            return View();
        }
    }
}
