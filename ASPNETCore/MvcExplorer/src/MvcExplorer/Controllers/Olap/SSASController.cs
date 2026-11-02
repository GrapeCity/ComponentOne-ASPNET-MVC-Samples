using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult SSAS()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }
    }
}