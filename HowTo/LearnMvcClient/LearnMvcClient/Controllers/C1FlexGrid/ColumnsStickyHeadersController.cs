using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsStickyHeaders
        public ActionResult ColumnsStickyHeaders()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}