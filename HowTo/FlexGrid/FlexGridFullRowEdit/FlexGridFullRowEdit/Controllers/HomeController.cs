using FlexGridFullRowEdit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace FlexGridFullRowEdit.Controllers
{
    public class HomeController : Controller
    {
        private static List<Sale> Source = Sale.GetData(10).ToList<Sale>();

        public ActionResult Index()
        {
            return View(Source);
        }

        [HttpPost]
        public JsonResult GetCountries()
        {
            var countries = Sale.GetCountries();
            return Json(countries);
        }

        [HttpPost]
        public JsonResult GetProducts()
        {
            var products = Sale.GetProducts();
            return Json(products);
        }

        public ActionResult GridEditorsUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = Source.Find(item => item.ID == sale.ID);
                fSale.Country = sale.Country;
                fSale.Amount = sale.Amount;
                fSale.Start = sale.Start;
                fSale.End = sale.End;
                fSale.Product = sale.Product;
                fSale.Active = sale.Active;

                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = fSale
                };
            }, () => Source));
        }

        public ActionResult GridEditorsDelete([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, item => {
                string error = string.Empty;
                bool success = true;

                var deleteItem = Source.Find(e => e.ID == item.ID);
                if (deleteItem != null)
                {
                    Source.Remove(deleteItem);
                }

                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = item
                };
            }, () => Source));
        }
    }
}
