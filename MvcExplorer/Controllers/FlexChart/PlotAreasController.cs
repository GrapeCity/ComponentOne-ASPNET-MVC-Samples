using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult PlotAreas()
        {
            return View(AVDRelation.getGata(20));
        }
    }
}
