using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ReadOnlyRequiredColumns
        public ActionResult ReadOnlyRequiredColumns()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}