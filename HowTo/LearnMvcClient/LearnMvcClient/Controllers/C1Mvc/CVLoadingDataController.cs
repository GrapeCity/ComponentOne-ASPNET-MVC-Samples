using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVLoadingData
        public ActionResult CVLoadingData()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}