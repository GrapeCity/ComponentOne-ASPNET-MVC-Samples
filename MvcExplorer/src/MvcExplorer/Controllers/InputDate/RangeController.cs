using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class InputDateController : Controller
    {
        public ActionResult Range()
        {
            return View();
        }
    }
}
