using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsProperties
        public ActionResult ColumnsProperties()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}