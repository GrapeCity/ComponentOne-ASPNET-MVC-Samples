using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IList<ITreeItem> treeViewData = null;
        private static IEnumerable<Sale> normalData = Sale.GetData(25);
        
        public ActionResult PDFExport()
        {
            if (treeViewData == null)
            {
                treeViewData = Folder.Create(Startup.Environment.WebRootPath).Children;
            }

            ViewBag.GroupedFlexGridData = normalData;
            ViewBag.MergedFlexGridData = normalData;
            ViewBag.TreeViewData = treeViewData;
            return View();
        }
    }
}