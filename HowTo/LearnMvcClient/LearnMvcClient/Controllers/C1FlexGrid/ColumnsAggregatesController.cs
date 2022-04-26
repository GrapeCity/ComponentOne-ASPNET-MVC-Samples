using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsAggregates
        public ActionResult ColumnsAggregates()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}