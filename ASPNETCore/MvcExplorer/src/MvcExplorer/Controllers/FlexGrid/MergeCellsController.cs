using System.Collections.Generic;
using C1.Web.Mvc.Grid;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

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
                    {"ExpandSelectionOnCopyPaste", new object[]{true, false}},
                    {"SkipMerged", new object[]{false, true}}
                }
            };
            return View();
        }

        public ActionResult MergeCells_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}
