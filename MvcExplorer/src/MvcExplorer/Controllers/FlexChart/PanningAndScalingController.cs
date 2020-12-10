using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController: Controller
    {
        private static IEnumerable<FinanceData> fbData = JsonDataReader.GetFromAssembly<FinanceData>("fb2.json");
        public ActionResult PanningAndScaling()
        {
            return View(fbData);
        }
    }
}
