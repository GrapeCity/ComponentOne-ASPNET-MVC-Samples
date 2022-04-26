using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsStyling
        public ActionResult ColumnsStyling()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}