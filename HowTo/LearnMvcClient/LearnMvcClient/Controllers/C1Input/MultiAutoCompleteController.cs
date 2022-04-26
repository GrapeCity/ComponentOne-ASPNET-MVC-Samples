using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: MultiAutoComplete
        public ActionResult MultiAutoComplete()
        {
            return View(Models.Gdp.GetData());
        }
    }
}