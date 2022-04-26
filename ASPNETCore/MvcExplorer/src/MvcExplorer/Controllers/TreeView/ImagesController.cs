using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: Images
        public ActionResult Images()
        {
            return View(Property.GetData(Url));
        }
    }
}