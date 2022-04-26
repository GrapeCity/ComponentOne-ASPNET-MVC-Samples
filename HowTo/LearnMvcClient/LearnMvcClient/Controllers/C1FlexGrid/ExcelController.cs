using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Excel
        public ActionResult Excel()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}