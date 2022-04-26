using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: ComboBoxObjects
        public ActionResult ComboBoxObjects()
        {
            return View(Models.InputData.GetSales());
        }
    }
}