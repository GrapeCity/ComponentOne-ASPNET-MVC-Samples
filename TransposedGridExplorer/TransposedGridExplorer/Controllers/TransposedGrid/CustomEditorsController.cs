using System.Web.Mvc;
using TransposedGridExplorer.Models;

namespace TransposedGridExplorer.Controllers
{
    public partial class TransposedGridController : Controller
    {
        public ActionResult CustomEditors()
        {
            ViewBag.Categories = ReleasePlan.GetCategories();
            return View(ReleasePlan.GetData());
        }
    }
}
