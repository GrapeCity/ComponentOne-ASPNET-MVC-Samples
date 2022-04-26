using C1.Web.Mvc;
using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private static List<Sale> Source = Sale.GetData(10).ToList<Sale>();
        public ActionResult CustomEditors()
        {
            ViewBag.Countries = Sale.GetCountries();
            ViewBag.Products = Sale.GetProducts();
            return View(Source);
        }

        public ActionResult MultiRowEditorsUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                Sale fSale = Source.Find(item => item.ID == sale.ID);
                fSale.Country = sale.Country;
                fSale.Amount = sale.Amount;
                fSale.Start = sale.Start;
                fSale.End = sale.End;
                fSale.Product = sale.Product;
                fSale.Active = sale.Active;
                fSale.Amount2 = sale.Amount2;
                fSale.Color = sale.Color;
                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = fSale
                };
            }, () => Source));
        }

        public ActionResult MultiRowEditorsCreate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    Source.Add(item);
                    item.ID = Source.Max(u => u.ID) + 1;
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => Source));
        }

        public ActionResult MultiRowEditorsDelete([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    Sale resultItem = Source.Find(u => u.ID == item.ID);
                    Source.Remove(resultItem);
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => Source));
        }
    }
}
