using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: DataLabels
        public ActionResult DataLabels()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}