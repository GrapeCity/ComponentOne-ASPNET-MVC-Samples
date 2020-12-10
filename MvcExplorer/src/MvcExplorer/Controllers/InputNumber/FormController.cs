using System;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class InputNumberController : Controller
    {
        private List<string> countries = Countries.GetCountries();
        private List<string> products = Products.GetProducts();
        public ActionResult Form()
        {
            ViewBag.Countries = countries;
            ViewBag.Products = products;
            var model = new CustomerOrder { ID = 101, OrderTime = DateTime.Now, Product = "PlayStation 4" };
            return View(model);
        }

        [HttpPost]
        public ActionResult Form(CustomerOrder model)
        {
            ViewBag.Countries = countries;
            ViewBag.Products = products;
            if (ModelState.IsValid)
            {
                ViewBag.Message = Localization.InputNumberRes.Form_Message;
            }
            return View(model);
        }
    }
}
