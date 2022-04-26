using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: AggregatesAboveData
        public ActionResult AggregatesAboveData()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}