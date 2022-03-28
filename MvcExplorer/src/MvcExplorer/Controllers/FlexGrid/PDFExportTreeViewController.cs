using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IList<ITreeItem> pdfExportTreeViewData = null;
        public ActionResult PDFExportTreeView()
        {
            if (pdfExportTreeViewData == null)
            {
                pdfExportTreeViewData = Folder.Create(Startup.Environment.WebRootPath).Children;
            }
            ViewBag.TreeViewData = pdfExportTreeViewData;
            return View();
        }
    }
}