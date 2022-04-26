using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class TreeMapController : Controller
    {
        // GET: GroupCollection
        public ActionResult GroupCollection()
        {
            return View(FoodSale.GetGroupData());
        }
    }
}