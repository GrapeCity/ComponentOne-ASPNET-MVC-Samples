using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: ControlsElements
        public ActionResult ControlsElements()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}