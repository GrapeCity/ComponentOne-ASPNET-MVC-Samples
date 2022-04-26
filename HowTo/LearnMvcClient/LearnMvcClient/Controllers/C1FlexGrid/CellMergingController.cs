using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: CellMerging
        public ActionResult CellMerging()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries4, 20));
        }
    }
}