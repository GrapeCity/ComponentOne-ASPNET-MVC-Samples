using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RestrictedMerging
        public ActionResult RestrictedMerging()
        {
            return View(Models.FlexGridData.GetSales(12));
        }
    }
}