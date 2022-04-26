using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVPaging
        public ActionResult CVPaging()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}