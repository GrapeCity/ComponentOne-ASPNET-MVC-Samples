using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public class ValidationController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View(new UserInfo() { Country = "" });
        }

        [HttpPost]
        public ActionResult Register(UserInfo userInfo)
        {
            ViewBag.IsValid = ModelState.IsValid;
            return View(userInfo);
        }
    }
}
