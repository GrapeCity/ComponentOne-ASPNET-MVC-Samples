using MvcExplorer.Models;
using System.Web.Mvc;

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