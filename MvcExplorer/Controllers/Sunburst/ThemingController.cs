using System;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class SunburstController : Controller
    {
        public ActionResult Theming()
        {
            return View(_data);
        }
    }
}
