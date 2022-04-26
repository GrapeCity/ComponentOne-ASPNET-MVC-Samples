using System;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class InputNumberController : Controller
    {
        public ActionResult ModelBinding()
        {
            var model = new CustomerOrder { ID = 101, Country = "China", Count = 5, OrderTime = DateTime.Now, Product = "PlayStation 4" };
            return View(model);
        }
    }
}
