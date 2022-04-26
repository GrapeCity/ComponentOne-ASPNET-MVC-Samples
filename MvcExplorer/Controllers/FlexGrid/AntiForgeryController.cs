using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult AntiForgery()
        {
            ViewBag.Countries = Sale.GetCountries();
            ViewBag.Products = Sale.GetProducts();
            return View(Source);
        }

        [C1AntiForgeryTokenAttribute]
        public ActionResult GridEditorsUpdateWithAntiForgery([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, sale =>
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

        [C1AntiForgeryTokenAttribute]
        public ActionResult GridEditorsCreateWithAntiForgery([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
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

        [C1AntiForgeryTokenAttribute]
        public ActionResult GridEditorsDeleteWithAntiForgery([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = Source.Find(u => u.ID == item.ID);
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
