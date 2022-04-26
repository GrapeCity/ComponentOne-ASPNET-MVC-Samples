using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ThemeDesigner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}