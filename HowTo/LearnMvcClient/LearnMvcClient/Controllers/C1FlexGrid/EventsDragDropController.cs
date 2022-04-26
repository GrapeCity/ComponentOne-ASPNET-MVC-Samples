using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: EventsDragDrop
        public ActionResult EventsDragDrop()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}