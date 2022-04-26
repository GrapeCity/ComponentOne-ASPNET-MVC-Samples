using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: TitleStyles
        public ActionResult TitleStyles()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}