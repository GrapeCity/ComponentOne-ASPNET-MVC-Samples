using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: MultiSelect
        public ActionResult MultiSelect()
        {
            return View(Models.Gdp.GetData());
        }
    }
}