using System.Web.Mvc;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult SSAS()
        {
            OlapModel.ControlId = "ssasPanel";
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }
    }
}