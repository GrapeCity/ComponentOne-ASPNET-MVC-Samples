using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexGrid101.Models;
using C1.Web.Mvc.Grid;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace FlexGrid101.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FlexGridModel model = new FlexGridModel();
            model.Settings = CreateSettings();
            model.CountryData = Sale.GetData(500);
            model.TreeData = Folder.Create(Server.MapPath("~")).Children;
            model.FilterBy = new string[] { "ID", "Country", "Product", "Color", "Start" };
            return View(model);
        }

        private IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"SelectionMode",new object[]{SelectionMode.None.ToString(),SelectionMode.Cell.ToString(),SelectionMode.CellRange.ToString(),
                    SelectionMode.Row.ToString(),SelectionMode.RowRange.ToString(),SelectionMode.ListBox.ToString(),SelectionMode.MultiRange.ToString()}},
                {"GroupBy", new object[]{"Country", "Product", "Color","Start","Amount","Country and Product","Product and Color", "None"}}
            };
            return settings;
        }

        public ActionResult GridRead([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, FlexGridModel.Source));
        }

        public ActionResult EGridUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = FlexGridModel.Source.Find(item => item.ID == sale.ID);
                fSale.Country = sale.Country;
                fSale.Amount = sale.Amount;
                fSale.Discount = sale.Discount;
                fSale.Active = sale.Active;
                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = fSale
                };
            }, () => FlexGridModel.Source));
        }

        public ActionResult NVGridUpdate([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, sale =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = FlexGridModel.Source.Find(item => item.ID == sale.ID);
                fSale.Country = sale.Country;
                fSale.Product = sale.Product;
                fSale.Amount = sale.Amount;
                fSale.Discount = sale.Discount;
                return new CollectionViewItemResult<Sale>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = fSale
                };
            }, () => FlexGridModel.Source));
        }
    }
}
