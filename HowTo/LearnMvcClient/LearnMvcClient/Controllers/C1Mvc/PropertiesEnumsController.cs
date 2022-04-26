using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: PropertiesEnums
        public ActionResult PropertiesEnums()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}