using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gauge101.Models;

namespace Gauge101.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(new GaugeModel());
        }

    }
}
