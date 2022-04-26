using Microsoft.AspNetCore.Mvc;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult SSAS()
        {
            OlapModel.ControlId = "ssasPanel";
            ViewBag.DemoOptions = OlapModel;
            return View();
        }
    }
}