using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

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
                ViewBag.Message = Resources.InputNumber.Form_Message;
            }
            return View(model);
        }
    }
}
