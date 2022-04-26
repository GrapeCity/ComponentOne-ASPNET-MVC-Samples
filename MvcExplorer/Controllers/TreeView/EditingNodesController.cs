using MvcExplorer.Models;
using System.Web.Mvc;

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