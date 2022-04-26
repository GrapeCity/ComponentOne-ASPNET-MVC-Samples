using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

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
