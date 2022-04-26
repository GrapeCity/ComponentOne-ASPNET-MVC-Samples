using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: AutoComplete
        public ActionResult AutoComplete()
        {
            return View(Models.Gdp.GetData());
        }
    }
}