using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Pdf
        public ActionResult Pdf()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}