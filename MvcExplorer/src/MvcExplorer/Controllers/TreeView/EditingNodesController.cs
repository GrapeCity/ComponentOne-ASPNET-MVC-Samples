using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: EditingNodes
        public ActionResult EditingNodes()
        {
            return View(Property.GetData(Url));
        }
    }
}