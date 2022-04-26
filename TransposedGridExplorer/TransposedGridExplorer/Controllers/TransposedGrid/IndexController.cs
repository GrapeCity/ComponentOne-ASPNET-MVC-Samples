using System.Web.Mvc;
using TransposedGridExplorer.Models;

namespace TransposedGridExplorer.Controllers
{
    public partial class TransposedGridController : Controller
    {
        public ActionResult Index()
        {
            return View(ProductSheet.GetData());
        }
    }
}
