using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class TabPanelController : Controller
    {
        // GET: DetachedPanels
        public ActionResult DetachedPanels(IFormCollection collection)
        {
            return View();
        }
    }
}