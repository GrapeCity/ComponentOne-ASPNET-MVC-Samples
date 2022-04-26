using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: SelectionFocus
        public ActionResult SelectionFocus()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}