using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsStylingVerticalAlignment
        public ActionResult RowsStylingVerticalAlignment()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}