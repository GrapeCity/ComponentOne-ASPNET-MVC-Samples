using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class CalendarController : Controller
    {
        public ActionResult Ranges()
        {
            ViewBag.DemoSettings = true;

            return View();
        }
    }
}
