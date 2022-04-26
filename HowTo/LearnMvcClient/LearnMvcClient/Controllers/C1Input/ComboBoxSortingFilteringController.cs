using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ComboBoxSortingFiltering
        public ActionResult ComboBoxSortingFiltering()
        {
            return View(Models.Gdp.GetData());
        }
    }
}