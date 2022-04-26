using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: AutoCompleteSearchPath
        public ActionResult AutoCompleteSearchPath()
        {
            return View(Models.Gdp.GetData());
        }
    }
}