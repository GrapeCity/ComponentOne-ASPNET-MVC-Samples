using LearnMvcClient.Models;
using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: HtmlEvents
        public ActionResult HtmlEvents()
        {
            ViewBag.Countries = Countries.GetCountries();
            return View();
        }
    }
}