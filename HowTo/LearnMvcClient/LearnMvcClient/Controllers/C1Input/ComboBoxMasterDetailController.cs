using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ComboBoxMasterDetail
        public ActionResult ComboBoxMasterDetail()
        {
            return View(Models.Gdp.GetData());
        }
    }
}