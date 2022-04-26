using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ComboBox
        public ActionResult ComboBox()
        {
            ViewBag.Countries = Models.InputData.GetCountries();
            return View(Models.InputData.GetSales());
        }
    }
}