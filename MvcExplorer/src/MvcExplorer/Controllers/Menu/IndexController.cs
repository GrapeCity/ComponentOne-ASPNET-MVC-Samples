using Microsoft.AspNetCore.Mvc;
using System;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MenuController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
