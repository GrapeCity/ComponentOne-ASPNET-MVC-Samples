using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class DashboardLayoutController : Controller
    {
        // GET: Index
        public ActionResult CustomTile()
        {
            return View(new DashboardData());
        }
    }
}