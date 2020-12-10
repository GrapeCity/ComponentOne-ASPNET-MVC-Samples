using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectController : Controller
    {
        private IList<string> products = Products.GetProducts();
        public ActionResult Form()
        {
            ViewBag.Products = products;
            var model = new CustomerProduct() { Products = new string[] { } };
            return View(model);
        }

        [HttpPost]
        public ActionResult Form(CustomerProduct model)
        {
            ViewBag.Products = products;
            if (model.Products != null && model.Products.Length > 0)
            {
                ViewBag.Message = string.Format(Localization.MultiSelectRes.Form_Message, string.Join(", ", model.Products));
            }
            return View(model);
        }
    }
}
