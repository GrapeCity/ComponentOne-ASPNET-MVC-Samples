using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<Sale> ValidationSales = Sale.GetValidationData().ToList();

        // GET: BuiltInValidation
        public ActionResult BuiltInValidation()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = CreateValidationSettings()
            };

            return View(ValidationSales);
        }

        private static IDictionary<string, object[]> CreateValidationSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ShowErrors", new object[]{ true, false }},
                {"ValidateEdits", new object[]{ true, false }},
                {"ErrorTipStyle", new object[]{
                        "red-yellow-errortip",
                        "gradient-errortip",
                        "text-style-errortip",
                        "strong-round-errortip"                        
                    }},
                {"TooltipPosition", new object[]{
                        "Above",
                        "AboveRight",
                        "RightTop",
                        "Right",
                        "RightBottom",
                        "BelowRight",
                        "Below",
                        "BelowLeft",
                        "LeftBottom",
                        "Left",
                        "LeftTop",
                        "AboveLeft"
                    }}
            };

            return settings;
        }

        public ActionResult GridUpdateSale([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = ValidationSales.Find(s => s.ID == item.ID);
                    var index = ValidationSales.IndexOf(resultItem);
                    ValidationSales.Remove(resultItem);
                    ValidationSales.Insert(index, item);
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
            }, () => ValidationSales));
        }

        public ActionResult GridCreateSale([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    ValidationSales.Add(item);
                    item.ID = ValidationSales.Max(s => s.ID) + 1;
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
            }, () => ValidationSales));
        }

        public ActionResult GridDeleteSale([C1JsonRequest]CollectionViewEditRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Sale>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = ValidationSales.Find(u => u.ID == item.ID);
                    ValidationSales.Remove(resultItem);
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
            }, () => ValidationSales));
        }
    }
}