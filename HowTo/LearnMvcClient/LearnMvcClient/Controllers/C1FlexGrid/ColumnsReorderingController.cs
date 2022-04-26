using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsReordering
        public ActionResult ColumnsReordering()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}