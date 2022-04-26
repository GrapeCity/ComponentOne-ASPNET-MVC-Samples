using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;
using C1.Web.Mvc.Serialization;


namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public static List<Sale> SALES = CustomerSale.GetData(100).ToList();
        public ActionResult DataMap()
        {
            ViewBag.Countries = CustomerSale.NAMEDCOUNTRIES;
            ViewBag.Products = CustomerSale.PRODUCTS;
            ViewBag.Colors = CustomerSale.COLORS;            
            ViewBag.Ranks = CustomerSale.RANKS;
            ViewBag.FullCountries = FullCountry.GetCountries();
            ViewBag.Cities = CitySale.GetCities();
            ViewBag.CitiesSales = CitySale.GetData(28);
            return View(SALES);
        }

        public ActionResult RemoteBindCustomerSale_Read([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, SALES, (col)=> 
            {
                if (col.Binding == "Country")
                {
                    return CustomerSale.NAMEDCOUNTRIES;
                }

                if (col.Binding == "Product")
                {
                    return CustomerSale.PRODUCTS;
                }

                if (col.Binding == "Color")
                {
                    return CustomerSale.COLORS;
                }
                                
                if (col.Binding == "Rank")
                {
                    return CustomerSale.RANKS;
                }

                return null;
            }));
        }

        public ActionResult ProductsUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = SALES.Find(item => item.ID == sale.ID);
                fSale.Active = sale.Active;
                fSale.Amount = sale.Amount;
                fSale.Color = sale.Color;
                fSale.Country = sale.Country;
                fSale.Discount = sale.Discount;
                fSale.End = sale.End;
                fSale.Amount2 = sale.Amount2;
                fSale.Start = sale.Start;
                fSale.Product = sale.Product;
                fSale.Rank = sale.Rank;
                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = fSale
                };
            }, () => SALES));
        }

    }
}
