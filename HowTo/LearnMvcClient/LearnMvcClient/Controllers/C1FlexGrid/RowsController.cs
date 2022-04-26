using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Rows
        public ActionResult Rows()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}