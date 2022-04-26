using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: AutoCompleteSearchParameters
        public ActionResult AutoCompleteSearchParameters()
        {
            return View(Models.Gdp.GetData());
        }
    }
}