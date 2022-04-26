using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColumnPicker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Sale.GetData(50));
        }

    }
}
