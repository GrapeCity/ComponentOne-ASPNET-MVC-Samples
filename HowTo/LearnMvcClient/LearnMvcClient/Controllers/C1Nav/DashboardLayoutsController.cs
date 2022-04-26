using LearnMvcClient.Models;
using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Adding
        public ActionResult DashboardLayouts()
        {
            return View();
        }
    }
}