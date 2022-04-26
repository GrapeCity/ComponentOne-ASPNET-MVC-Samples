using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ComboBoxStrings
        public ActionResult ComboBoxStrings()
        {
            return View(Models.InputData.GetCountries());
        }
    }
}