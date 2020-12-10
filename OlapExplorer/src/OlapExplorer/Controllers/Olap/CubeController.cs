using Microsoft.AspNetCore.Mvc;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        // GET: Cube
        public ActionResult Cube()
        {
            OlapModel.ControlId = "cubePanel";
            ViewBag.DemoOptions = OlapModel;
            return View();
        }
    }
}