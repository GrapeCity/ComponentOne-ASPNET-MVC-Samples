using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsSizing
        public ActionResult ColumnsSizing()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}