using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVTrackingChanges
        public ActionResult CVTrackingChanges()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}