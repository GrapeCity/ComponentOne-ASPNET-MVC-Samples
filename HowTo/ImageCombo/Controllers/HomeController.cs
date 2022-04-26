using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageCombo.Models;

namespace ImageCombo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Category.GetData());
        }
    }
}
