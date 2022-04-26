using MvcExplorer.Models;
using System.Web.Mvc;

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