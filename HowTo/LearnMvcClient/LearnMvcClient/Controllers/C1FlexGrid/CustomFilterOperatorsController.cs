using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: CustomFilterOperators
        public ActionResult CustomFilterOperators()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}