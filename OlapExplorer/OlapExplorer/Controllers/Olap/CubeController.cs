using System.Web.Mvc;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        // GET: Cube
        public ActionResult Cube()
        {
            OlapModel.ControlId = "cubePanel";
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }
    }
}