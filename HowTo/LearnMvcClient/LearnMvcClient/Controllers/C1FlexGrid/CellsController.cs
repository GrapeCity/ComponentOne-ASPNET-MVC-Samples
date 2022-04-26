using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Cells
        public ActionResult Cells()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}