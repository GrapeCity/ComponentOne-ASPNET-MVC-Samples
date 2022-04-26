using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsAutoSizing
        public ActionResult ColumnsAutoSizing()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}