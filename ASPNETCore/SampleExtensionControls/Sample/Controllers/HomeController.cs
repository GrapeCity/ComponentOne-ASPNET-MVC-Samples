using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using static Sample.Models.StaticModel;
namespace Sample.Controllers
{
    public class HomeController : Controller
    {

        IEnumerable<Country> countrylist;
        List<Country> ncountrylist;
        public ActionResult CountrySearch([C1JsonRequest] CollectionViewRequest<Country> requestData)
        {
            countrylist = GetCountriesForMultiColumnComboBox();
            ncountrylist = countrylist.ToList();
            IEnumerable<Country> selectedcountryList = ncountrylist;
            if (requestData.ExtraRequestData.Count > 0)
            {
                var query = requestData.ExtraRequestData["AutoCompleteQuery"] as string;

                if (query != null && query != "")
                {
                    var names = query.Split(',');
                    selectedcountryList = countrylist.Where(item => names.Any(n => item.CountryName.IndexOf(n, StringComparison.OrdinalIgnoreCase) >= 0));
                }
            }
            // Delay result
            System.Threading.Thread.Sleep(50);
            var resultList = selectedcountryList.Select(item => new Country
            {
                CountryName = item.CountryName,
                CountryShortName = item.CountryShortName,
                CountryCode = item.CountryCode
            });
            return this.C1Json(CollectionViewHelper.Read(requestData, resultList));
        }

        private static List<Sale> Source = Sale.GetData(10).ToList<Sale>();
        public ActionResult Index()
        {
            ViewBag.Countries = Sale.GetCountries();
            ViewBag.Products = Sale.GetProducts();
            return View();
        }

        public ActionResult CustomEditorsBind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Source));
        }

        public ActionResult GridEditorsUpdate([C1JsonRequest] CollectionViewEditRequest<Sale> requestData)
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

        public ActionResult GridEditorsCreate([C1JsonRequest] CollectionViewEditRequest<Sale> requestData)
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

        public ActionResult GridEditorsDelete([C1JsonRequest] CollectionViewEditRequest<Sale> requestData)
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