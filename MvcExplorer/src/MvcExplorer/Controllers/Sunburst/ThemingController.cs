using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;

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
