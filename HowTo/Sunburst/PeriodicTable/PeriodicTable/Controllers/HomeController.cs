using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeriodicTable.Models;

namespace PeriodicTable.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            PeriodicTableModel model = new PeriodicTableModel();
            return View(model);
        }
    }
}