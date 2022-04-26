using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: RowsDetails
        public ActionResult RowsDetails()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}