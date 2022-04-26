using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: AutoCompleteCustomSearch
        public ActionResult AutoCompleteCustomSearch()
        {
            return View(Models.Gdp.GetData());
        }
    }
}