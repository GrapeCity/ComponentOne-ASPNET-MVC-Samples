using System.Web.Mvc;
using System.Linq;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVStableSort
        public ActionResult CVStableSort()
        {
            var data = Models.FlexGridData.GetSales(200).OrderBy(s => s.Country);
            return View(data);
        }
    }
}