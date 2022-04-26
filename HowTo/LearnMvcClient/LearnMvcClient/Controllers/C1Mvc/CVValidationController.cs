using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVValidation
        public ActionResult CVValidation()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}