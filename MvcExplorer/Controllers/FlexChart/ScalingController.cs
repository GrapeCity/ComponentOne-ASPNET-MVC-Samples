using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
    {
        //
        // GET: /Scaling/

        public ActionResult Scaling()
        {
            return View(Population.GetData());
        }

    }
}
