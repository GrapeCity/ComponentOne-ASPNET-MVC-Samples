using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: CustomCells
        public ActionResult CustomCells()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries6, 200));
        }
    }
}