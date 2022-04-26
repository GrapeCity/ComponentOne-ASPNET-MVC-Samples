using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: Styling
        public ActionResult Styling()
        {
            return View(Property.GetData(Url));
        }
    }
}