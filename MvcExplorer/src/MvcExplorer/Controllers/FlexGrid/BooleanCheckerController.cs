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
        private static List<Sale> _source = Sale.GetData(20).ToList<Sale>();
        public ActionResult BooleanChecker()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"ShowCheckAll", new object[]{"True", "False"}},
                    {"ShowCheckGroups", new object[]{"True", "False"}}
                }
            };
            return View();
        }

        public ActionResult GridBooleanCheckerBind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _source));
        }

        public ActionResult GridBooleanCheckerUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = _source.Find(item => item.ID == sale.ID);
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
            }, () => _source));
        }
    }
}
