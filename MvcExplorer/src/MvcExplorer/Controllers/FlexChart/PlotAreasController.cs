using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

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
