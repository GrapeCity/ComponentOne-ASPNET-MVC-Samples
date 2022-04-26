using C1.Web.Mvc;
using HeaderFilters.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace HeaderFilters.Controllers
{
    public class HeaderFilterController : Controller
    {
        //
        // GET: /HeaderFilter/

        public ActionResult Index(FormCollection collection)
        {
            string productName = string.Empty;
            string countryName = string.Empty;
            double? price = null;
            IEnumerable<Sale> source = Sale.GetData(50);
            ViewBag.Countries = HeaderFilters.Models.Sale.GetCountries();

            IValueProvider data = collection;
            if (CallbackManager.CurrentIsCallback)
            {
                var request = CallbackManager.GetCurrentCallbackData<CollectionViewRequest<Sale>>();
                if (request != null && request.ExtraRequestData != null)
                {
                    Hashtable extraData = request.ExtraRequestData as Hashtable;
                    if (extraData != null && extraData["HeaderFilters"] != null)
                    {
                        Hashtable headerFiltersData = extraData["HeaderFilters"] as Hashtable;
                        foreach (DictionaryEntry item in headerFiltersData)
                        {
                            if (item.Value == null)
                            {
                                continue;
                            }
                            switch ((string)item.Key)
                            {
                                case "product":
                                    productName = (string)item.Value;
                                    break;
                                case "country":
                                    countryName = (string)item.Value;
                                    break;
                                case "price":
                                    price = double.Parse(item.Value.ToString());
                                    break;
                                default:
                                    break;
                            }
                        }
                        return this.C1Json(CollectionViewHelper.Read<Sale>(request, GetFilteredData(source, productName, countryName, price).ToList<Sale>()));
                    }
                }
            }
            return View(source);
        }

        private IEnumerable<Sale> GetFilteredData(IEnumerable<Sale> source, string productName, string countryName, double? price)
        {
            if (!string.IsNullOrEmpty(productName) || !string.IsNullOrEmpty(countryName) || price.HasValue)
            {
                return source.Where<Sale>(item =>
                {
                    if (!string.IsNullOrEmpty(productName))
                    {
                        if (item.Product.IndexOf(productName, StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(countryName))
                    {
                        if (string.Compare(item.Country, countryName, true) != 0)
                        {
                            return false;
                        }
                    }
                    if (price.HasValue)
                    {
                        if (item.Price > price.Value)
                        {
                            return false;
                        }
                    }
                    return true;
                });
            }
            return source;
        }

    }
}
