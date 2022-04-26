using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsAddingRemoving
        public ActionResult RowsAddingRemoving()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}