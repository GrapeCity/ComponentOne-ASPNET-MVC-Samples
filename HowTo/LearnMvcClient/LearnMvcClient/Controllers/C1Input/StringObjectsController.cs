using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: StringObjects
        public ActionResult StringObjects()
        {
            return View(Models.PopulationGdp.GetData());
        }
    }
}