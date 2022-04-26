using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVAddingRemoving
        public ActionResult CVAddingRemoving()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}