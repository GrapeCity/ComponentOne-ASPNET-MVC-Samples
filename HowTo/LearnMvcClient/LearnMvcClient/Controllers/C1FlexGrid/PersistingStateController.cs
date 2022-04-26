using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: PersistingState
        public ActionResult PersistingState()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}