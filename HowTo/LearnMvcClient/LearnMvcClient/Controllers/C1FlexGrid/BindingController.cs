using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Binding
        public ActionResult Binding()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}