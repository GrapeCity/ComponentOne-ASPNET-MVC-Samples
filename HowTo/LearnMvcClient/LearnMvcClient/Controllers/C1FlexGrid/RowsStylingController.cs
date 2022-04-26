using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsStyling
        public ActionResult RowsStyling()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}