using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: AggregatesBelowData
        public ActionResult AggregatesBelowData()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}