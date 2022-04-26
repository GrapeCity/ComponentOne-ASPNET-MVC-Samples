using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ImportExport
        public ActionResult ImportExport()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}