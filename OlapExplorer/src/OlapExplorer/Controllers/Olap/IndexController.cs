using OlapExplorer.Models;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using C1.Web.Mvc.Olap;

namespace OlapExplorer.Controllers.Olap
{
    public partial class OlapController : Controller
    {
        private static IEnumerable Data = ProductData.GetData(1000).ToList();

        private readonly ClientSettingsModel OlapModel = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"RowTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ColumnTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ShowZeros", new object[] { false, true } },
                { "AllowMerging", new object[] {
                    C1.Web.Mvc.Grid.AllowMerging.All,
                    C1.Web.Mvc.Grid.AllowMerging.AllHeaders,
                    C1.Web.Mvc.Grid.AllowMerging.Cells,
                    C1.Web.Mvc.Grid.AllowMerging.ColumnHeaders,
                    C1.Web.Mvc.Grid.AllowMerging.None,
                    C1.Web.Mvc.Grid.AllowMerging.RowHeaders
                } }
            }
        };

        // GET: PivotGrid
        public ActionResult Index()
        {
            OlapModel.ControlId = "indexPanel";
            ViewBag.DemoOptions = OlapModel;
            return View(Data);
        }
    }
}