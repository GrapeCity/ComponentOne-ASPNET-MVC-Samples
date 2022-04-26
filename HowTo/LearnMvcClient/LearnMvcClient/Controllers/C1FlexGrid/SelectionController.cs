using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Selection
        public ActionResult Selection()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}