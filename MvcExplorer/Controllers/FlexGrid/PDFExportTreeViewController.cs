using C1.Web.Mvc;
using C1.Web.Mvc.Grid;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IList<ITreeItem> pdfExportTreeViewData = null;
        public ActionResult PDFExportTreeView()
        {
            if (pdfExportTreeViewData == null)
            {
                pdfExportTreeViewData = MvcExplorer.Models.Folder.Create(Server.MapPath("~")).Children;
            }
            ViewBag.TreeViewData = pdfExportTreeViewData;           
            return View();
        }
    }
}