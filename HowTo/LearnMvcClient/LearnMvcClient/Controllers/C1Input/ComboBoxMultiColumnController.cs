using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ComboBoxMultiColumn
        public ActionResult ComboBoxMultiColumn()
        {
            return View(Models.Gdp.GetData());
        }
    }
}