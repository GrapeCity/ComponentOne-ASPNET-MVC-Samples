using C1.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<Sale> Source = Sale.GetData(20).ToList<Sale>();
        public ActionResult CustomEditors()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = _getCustomEditorClientSettings(),
                DefaultValues = _getCustomEditorDefaultValues()
            };

            ViewBag.Countries = Sale.GetCountries();
            ViewBag.Products = Sale.GetProducts();
            return View();
        }

        private static IDictionary<string, object[]> _getCustomEditorClientSettings()
        {
            return new Dictionary<string, object[]>
            {
                {"KeyActionTab", new object[]{ "None", "MoveDown", "MoveAcross", "Cycle", "CycleOut", "CycleEditable" }},
                {"KeyActionEnter", new object[]{ "None", "MoveDown", "MoveAcross", "Cycle", "CycleOut", "CycleEditable" }}
            };
        }

        private static IDictionary<string, object> _getCustomEditorDefaultValues()
        {
            var defaultValues = new Dictionary<string, object>
            {
                {"KeyActionTab", "CycleEditable"},
                {"KeyActionEnter", "MoveDown"}
            };

            return defaultValues;
        }

        public ActionResult CustomEditorsBind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Source));
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
