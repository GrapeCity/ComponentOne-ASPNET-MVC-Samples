using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Sorting
        public ActionResult Sorting()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}