using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Input101.Models;

namespace Input101.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new InputModel());
        }        
    }
}
