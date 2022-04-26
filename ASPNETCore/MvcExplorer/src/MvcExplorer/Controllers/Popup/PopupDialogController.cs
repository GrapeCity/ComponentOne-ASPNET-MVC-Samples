using Microsoft.AspNetCore.Mvc;
using System;
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
