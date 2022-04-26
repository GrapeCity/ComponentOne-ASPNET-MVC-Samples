using C1.Web.Mvc.Olap;
using Olap101.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Olap101.Controllers
{
    public class HomeController : Controller
    {
        private IDictionary<string, object[]> GetSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"RowTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ColumnTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ShowZeros", new object[] { false, true } },
                {"TotalsBeforeData", new object[] { false, true } },
                {"ChartType", new object[] { PivotChartType.Column, PivotChartType.Area, PivotChartType.Bar, PivotChartType.Line, PivotChartType.Pie, PivotChartType.Scatter}.Select(item => item.ToString()).ToArray() },
            };
            return settings;
        }

        public ActionResult Index()
        {
            OLAP101Model model = new OLAP101Model();
            model.Data = ProductData.GetData(10000).ToList();
            model.Settings = GetSettings();
            model.ControlId = "indexPanel";
            return View(model);
        }
    }
}