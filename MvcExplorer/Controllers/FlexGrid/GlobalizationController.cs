using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult Globalization()
        {
            return View(Sale.GetData(50));
        }
    }
}
