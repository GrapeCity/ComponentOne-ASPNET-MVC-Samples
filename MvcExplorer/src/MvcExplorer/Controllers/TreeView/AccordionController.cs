using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

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