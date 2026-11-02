using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Data;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;
using System.Data.Entity;
using System.Web.Http;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        private C1NWindEntities db = new C1NWindEntities();
        public ActionResult MultiSelect()
        {
            return View(db);
        }

        public ActionResult ListUpdateProducts([C1JsonRequest]CollectionViewEditRequest<Product> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Product>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    db.Entry(item as object).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    error = string.Join(",", e.EntityValidationErrors.Select(result =>
                    {
                        return string.Join(",", result.ValidationErrors.Select(err => err.ErrorMessage));
                    }));
                    success = false;
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<Product>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = item
                };
            }, () => db.Products.ToList<Product>()));
        }

        public ActionResult UpdateCheckedItems([FromBody] List<int> checkedItems)
        {
            try
            {
                // Update the database based on the checked items
                checkedItems = checkedItems ?? new List<int>();
                var products = db.Products.ToList();
                foreach (var product in products)
                {
                    product.Discontinued = checkedItems.Contains(product.ProductID);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
