using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE31
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<SaleShowCase> model;
        private readonly ControlOptions _showcaseOption = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Data Size",new OptionItem{Values = new List<string> {"5 " + Localization.FlexGridRes.ShowCase_Rows_Text0,
                    "50 " + Localization.FlexGridRes.ShowCase_Rows_Text0, "500 " + Localization.FlexGridRes.ShowCase_Rows_Text0,
                    "5000 " + Localization.FlexGridRes.ShowCase_Rows_Text0, "50000 " + Localization.FlexGridRes.ShowCase_Rows_Text0,
                    "100000 " + Localization.FlexGridRes.ShowCase_Rows_Text0},CurrentValue = "500 " + Localization.FlexGridRes.ShowCase_Rows_Text0}},
                {"Lazy Render",new OptionItem{Values = new List<string> {"True", "False"},CurrentValue = "True"}}
            }
        };
        public ActionResult ShowCase(IFormCollection collection)
        {
            _showcaseOption.LoadPostData(collection);
            ViewBag.DemoOptions = _showcaseOption;
            ViewBag.Countries = FullCountry.GetCountries();
            ViewBag.Products = ProductObject.GetProductObjects();
            ViewBag.Colors = ColorObject.GetColorObjects();
            Theme.SetCurrentTheme(HttpContext, Themes.CleanLight);
            return View(model);
        }
        public ActionResult ShowCase_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _showcaseOption.LoadPostData(data);
            model = Sale.GetData(getDataSize()).Select(x => SaleShowCase.FromSale(x)).ToList();
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
                    fSale.Rank = sale.Rank;
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
            int dataSize = Int32.Parse(_showcaseOption.Options["Data Size"].CurrentValue.Split(' ')[0]);
            return dataSize;
        }
    }
}
