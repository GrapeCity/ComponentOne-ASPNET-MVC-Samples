using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

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