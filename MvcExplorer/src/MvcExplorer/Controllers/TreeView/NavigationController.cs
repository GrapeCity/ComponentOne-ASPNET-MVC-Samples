using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

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