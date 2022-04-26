using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Paging
        public ActionResult Paging()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}