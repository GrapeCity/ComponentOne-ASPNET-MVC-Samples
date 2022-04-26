using LearnMvcClient.Models;
using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: Events
        public ActionResult Events()
        {
            ViewBag.Countries = Countries.GetCountries();
            return View();
        }
    }
}