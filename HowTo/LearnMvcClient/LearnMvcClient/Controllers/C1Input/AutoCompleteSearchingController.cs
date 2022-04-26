using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: AutoCompleteSearching
        public ActionResult AutoCompleteSearching()
        {
            return View(Models.Gdp.GetData());
        }
    }
}