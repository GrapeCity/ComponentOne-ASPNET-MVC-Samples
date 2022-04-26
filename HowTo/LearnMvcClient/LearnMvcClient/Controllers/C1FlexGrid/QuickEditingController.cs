using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: QuickEditing
        public ActionResult QuickEditing()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}