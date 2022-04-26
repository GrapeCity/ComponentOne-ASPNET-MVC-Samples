using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: MultipleChartTypes
        public ActionResult MultipleChartTypes()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}