using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        // GET: /<controller>/
        public ActionResult Grouping()
        {
            return View(_db.Orders.Take(20).ToList());
        }
    }
}