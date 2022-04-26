using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectController : Controller
    {
        private C1NWindEntities _db;
        public MultiSelectController(C1NWindEntities db)
        {
            _db = db;
        }
        public ActionResult ComplexType()
        {
            return View(_db);
        }
    }
}

