using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class InputDateRangeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.DemoSettings = true;
            return View();
        }
    }
}
