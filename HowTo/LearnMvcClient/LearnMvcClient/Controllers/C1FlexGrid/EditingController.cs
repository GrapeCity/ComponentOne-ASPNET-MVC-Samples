using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Editing
        public ActionResult Editing()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}