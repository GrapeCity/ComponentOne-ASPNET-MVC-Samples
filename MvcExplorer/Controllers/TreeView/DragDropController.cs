using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: DragDrop
        public ActionResult DragDrop()
        {
            return View(Property.GetData(Url));
        }
    }
}