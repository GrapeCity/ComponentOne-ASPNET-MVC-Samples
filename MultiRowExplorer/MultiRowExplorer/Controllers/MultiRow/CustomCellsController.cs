using System.Web.Mvc;
using MultiRowExplorer.Models;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult CustomCells()
        {
            return View(Sale.GetData(500));
        }
    }
}
