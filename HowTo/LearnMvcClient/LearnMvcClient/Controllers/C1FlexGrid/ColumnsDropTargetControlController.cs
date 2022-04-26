using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsDropTargetControl
        public ActionResult ColumnsDropTargetControl()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}