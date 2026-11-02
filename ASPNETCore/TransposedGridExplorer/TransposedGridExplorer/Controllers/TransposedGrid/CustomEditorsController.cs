using Microsoft.AspNetCore.Mvc;
using TransposedGridExplorer.Models;

namespace TransposedGridExplorer.Controllers
{
    public partial class TransposedGridController : Controller
    {
        public IActionResult CustomEditors()
        {
            ViewBag.Categories = ReleasePlan.GetCategories();
            return View(ReleasePlan.GetData());
        }
    }
}
