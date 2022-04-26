using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsStylingHover
        public ActionResult RowsStylingHover()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}