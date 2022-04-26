using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsProperties
        public ActionResult RowsProperties()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}