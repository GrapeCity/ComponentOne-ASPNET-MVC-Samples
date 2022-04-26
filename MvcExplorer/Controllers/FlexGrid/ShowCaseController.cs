using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;
using System;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<SaleShowCase> model;
        private readonly ControlOptions _showcaseOption = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Data Size",new OptionItem{Values = new List<string> {"5 " + Resources.FlexGrid.ShowCase_Rows_Text0,
                    "50 " + Resources.FlexGrid.ShowCase_Rows_Text0, "500 " + Resources.FlexGrid.ShowCase_Rows_Text0,
                    "5000 " + Resources.FlexGrid.ShowCase_Rows_Text0, "50000 " + Resources.FlexGrid.ShowCase_Rows_Text0,
                    "100000 " + Resources.FlexGrid.ShowCase_Rows_Text0},CurrentValue = "500 " + Resources.FlexGrid.ShowCase_Rows_Text0}},
                {"Lazy Render",new OptionItem{Values = new List<string> {"True", "False"},CurrentValue = "True"}}
            }
        };
        public ActionResult ShowCase(FormCollection collection)
        {
            IValueProvider data = collection;
            if (CallbackManager.CurrentIsCallback)
            {
                var request = CallbackManager.GetCurrentCallbackData<CollectionViewRequest<object>>();
                if (request != null && request.ExtraRequestData != null)
                {
                    var extraData = request.ExtraRequestData.Cast<DictionaryEntry>()
                        .ToDictionary(kvp => (string)kvp.Key, kvp => kvp.Value.ToString());
                    data = new DictionaryValueProvider<string>(extraData, CultureInfo.CurrentCulture);
                }
            }
            _showcaseOption.LoadPostData(data);
            model = Sale.GetData(getDataSize()).Select(x => SaleShowCase.FromSale(x)).ToList();
            ViewBag.DemoOptions = _showcaseOption;
            ViewBag.Countries = FullCountry.GetCountries();
            ViewBag.Products = ProductObject.GetProductObjects();
            ViewBag.Colors = ColorObject.GetColorObjects();
            Theme.CurrentTheme = Themes.CleanLight;
            return View(model);
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
