using System.Web.Mvc;
using System.Collections.Generic;
using C1.Web.Mvc.Grid;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult MergeCells()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"AllowMerging", new object[]{AllowMerging.All, AllowMerging.None, AllowMerging.Cells, AllowMerging.ColumnHeaders, AllowMerging.RowHeaders, AllowMerging.AllHeaders}},
                    {"ExpandSelectionOnCopyPaste", new object[]{true, false}}
                }
            };
            return View(Sale.GetData(500));
        }
    }
}
