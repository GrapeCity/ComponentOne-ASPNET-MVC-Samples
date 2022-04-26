using Microsoft.AspNetCore.Mvc;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult DataSource()
        {
            OlapModel.ControlId = "dataSourcePanel";
            ViewBag.DemoOptions = OlapModel;
            return View();
        }
    }
}