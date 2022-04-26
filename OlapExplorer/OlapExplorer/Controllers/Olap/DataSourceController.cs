using System.Web.Mvc;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult DataSource()
        {
            OlapModel.ControlId = "dataSourcePanel";
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }
    }
}