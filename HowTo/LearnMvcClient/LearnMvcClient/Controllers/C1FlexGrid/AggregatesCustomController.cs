using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: AggregatesCustom
        public ActionResult AggregatesCustom()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}