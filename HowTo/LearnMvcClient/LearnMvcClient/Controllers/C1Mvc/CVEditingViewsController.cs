using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVEditingViews
        public ActionResult CVEditingViews()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}