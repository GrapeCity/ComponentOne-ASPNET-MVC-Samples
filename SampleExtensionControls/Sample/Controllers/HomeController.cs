using C1.Web.Mvc;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private static List<Sale> Source = Sale.GetData(10).ToList<Sale>();
        IEnumerable<Country> countrylist;
        List<Country> ncountrylist;
        // GET: FlexGrid
        public ActionResult Index()
        {
            ViewBag.Products = Sale.GetProducts();
            return View(Source);
        }

        public ActionResult CountrySearch([C1JsonRequest] CollectionViewRequest<Country> requestData)
        {
            countrylist = Sale.GetCountries();
            ncountrylist = countrylist.ToList();
            var query = requestData.ExtraRequestData["AutoCompleteQuery"] as string;
            IEnumerable<Country> selectedcountryList = ncountrylist;
            if (query != null && query != "")
            {
                var names = query.Split(',');
                selectedcountryList = countrylist.Where(item => names.Any(n => item.CountryName.IndexOf(n, StringComparison.OrdinalIgnoreCase) >= 0));
            }
            // Delay result
            System.Threading.Thread.Sleep(1000);
            var resultList = selectedcountryList.Select(item => new Country { CountryName = item.CountryName,
                CountryShortName = item.CountryShortName, CountryCode = item.CountryCode });
            return this.C1Json(CollectionViewHelper.Read(requestData, resultList));
        }
        public ActionResult GridEditorsUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
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

        public ActionResult GridEditorsCreate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
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

        public ActionResult GridEditorsDelete([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
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
