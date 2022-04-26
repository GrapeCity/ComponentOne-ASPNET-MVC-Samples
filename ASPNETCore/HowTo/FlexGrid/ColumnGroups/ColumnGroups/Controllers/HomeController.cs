using Microsoft.AspNetCore.Mvc;

namespace ColumnGroups.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
