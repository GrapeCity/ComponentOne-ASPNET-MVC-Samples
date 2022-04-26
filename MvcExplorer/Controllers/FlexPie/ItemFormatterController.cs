using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult ItemFormatter()
        {
            return View(CustomerOrder.GetCountryGroupOrderData());
        }
    }
}