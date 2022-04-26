using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsStylingRowHeight
        public ActionResult RowsStylingRowHeight()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}