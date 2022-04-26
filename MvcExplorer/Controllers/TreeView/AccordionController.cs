using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: Accordion
        public ActionResult Accordion()
        {
            return View(MenuItemData.GetData());
        }
    }
}