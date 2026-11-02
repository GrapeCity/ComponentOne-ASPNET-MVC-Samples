using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult Index()
        {
            Theme.SetCurrentTheme(HttpContext, Themes.Default);
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
