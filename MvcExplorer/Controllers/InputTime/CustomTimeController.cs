using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class InputTimeController : Controller
    {
        public ActionResult CustomTime()
        {
            ViewBag.TimeList = new List<object> { "8:20", "10:00", "11:35", "12:08", "13:25", "13:30", "14:26" };
            return View();
        }
    }
}
