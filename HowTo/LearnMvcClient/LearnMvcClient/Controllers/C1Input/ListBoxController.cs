using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ListBox
        public ActionResult ListBox()
        {
            return View(Models.Gdp.GetData());
        }
    }
}