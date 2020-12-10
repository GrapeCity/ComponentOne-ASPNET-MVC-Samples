using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE30 && !NET50
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<SaleProductDetail> model;
        private readonly ControlOptions _showcaseOption = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Data Size",new OptionItem{Values = new List<string> {"5 Rows", "50 Rows", "500 Rows", "5000 Rows", "50000 Rows", "100000 Rows", "500000 Rows", "1000000 Rows"},CurrentValue = "500 Rows"}}
            }
        };
        public ActionResult ShowCase(IFormCollection collection)
        {
            _showcaseOption.LoadPostData(collection);
            ViewBag.DemoOptions = _showcaseOption;
            ViewBag.Countries = FullCountry.GetCountries();
            ViewBag.Products = Sale.GetProducts();
            ViewBag.Colors = Sale.GetColors();
            Theme.SetCurrentTheme(HttpContext, Themes.CleanLight);
            return View(model);
        }
        public ActionResult ShowCase_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _showcaseOption.LoadPostData(data);
            model = Sale.GetData(getDataSize()).Select(x => SaleProductDetail.FromSale(x)).ToList();
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
        public ActionResult GridShowCaseUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var fSale = model.Find(item => item.ID == sale.ID);
                    fSale.Country = sale.Country;
                    fSale.Amount = sale.Amount;
                    fSale.Start = sale.Start;
                    fSale.End = sale.End;
                    fSale.Product = sale.Product;
                    fSale.Active = sale.Active;
                    fSale.Amount2 = sale.Amount2;
                    fSale.Color = sale.Color;
                    fSale.Discount = sale.Discount;
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
                    Data = sale
                };
            }, () => model));
        }

        private int getDataSize()
        {
            int dataSize = 0;
            var dataSizeOption = _showcaseOption.Options["Data Size"].CurrentValue;
            switch (dataSizeOption)
            {
                case "5 Rows":
                    dataSize = 5;
                    break;
                case "50 Rows":
                    dataSize = 50;
                    break;
                case "500 Rows":
                    dataSize = 500;
                    break;
                case "5000 Rows":
                    dataSize = 5000;
                    break;
                case "50000 Rows":
                    dataSize = 50000;
                    break;
                case "100000 Rows":
                    dataSize = 100000;
                    break;
                case "500000 Rows":
                    dataSize = 500000;
                    break;
                case "1000000 Rows":
                    dataSize = 1000000;
                    break;
            }
            return dataSize;
        }
    }
}
