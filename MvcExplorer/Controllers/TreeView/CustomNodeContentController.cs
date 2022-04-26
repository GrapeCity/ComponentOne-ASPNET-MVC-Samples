using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController
    {
        // GET: CustomNodeContent
        public ActionResult CustomNodeContent()
        {
            return View(Property.GetData(Url));
        }
    }
}