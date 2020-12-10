using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesExplorer.Models;

namespace RazorPagesExplorer.Pages
{
    public class InputModel : PageModel
    {
        public readonly List<string> Countries = Models.Countries.GetCountries();
        public readonly List<string> Products = Models.Products.GetProducts();
        private CustomerOrder _customerOrder= new CustomerOrder { ID = 101, OrderTime = DateTime.Now, Product = "PlayStation 4" };
        [BindProperty]
        public CustomerOrder CustomerOrder
        {
            get { return _customerOrder; }
            set { _customerOrder = value; }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ViewData["Message"] = "CustomerOrder updated successfully, thanks!";
            }
        }
    }
}