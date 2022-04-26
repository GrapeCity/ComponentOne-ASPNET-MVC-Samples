using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

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