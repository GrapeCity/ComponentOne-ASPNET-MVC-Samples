using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Columns
        public ActionResult Columns()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}