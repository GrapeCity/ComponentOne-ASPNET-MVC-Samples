using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class TreeMapController : Controller
    {
        // GET: Theming
        public ActionResult Theming()
        {
            return View(FoodSale.GetDate());
        }
    }
}