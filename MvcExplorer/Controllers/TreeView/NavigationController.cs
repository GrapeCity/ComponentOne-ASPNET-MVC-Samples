using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: Navigation
        public ActionResult Navigation()
        {
            return View(Property.GetData(Url));
        }
    }
}