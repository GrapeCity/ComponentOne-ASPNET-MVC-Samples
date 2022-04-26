using MvcExplorer.Models;
using System.Web.Mvc;

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