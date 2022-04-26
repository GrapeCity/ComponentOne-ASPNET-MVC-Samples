using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ColumnsColumnLayout
        public ActionResult ColumnsColumnLayout()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}