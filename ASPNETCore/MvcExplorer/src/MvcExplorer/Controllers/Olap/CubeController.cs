using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    partial class OlapController : Controller
    {
        // GET: Cube
        public ActionResult Cube()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }
    }
}