using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ListBoxCheckBoxes
        public ActionResult ListBoxCheckBoxes()
        {
            return View(Models.Gdp.GetData());
        }
    }
}