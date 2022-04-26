using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private C1NWindEntities db = new C1NWindEntities();
        //
        // GET: /Editing/

        public ActionResult Editing()
        {
            return View(db);
        }

        public ActionResult GridUpdateCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            return Update(requestData, db.Categories);
        }

        public ActionResult GridCreateCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            var category = requestData.OperatingItems.First();
            if (category.CategoryName == null)
            {
                category.CategoryName = "";
            }

            return Create(requestData, db.Categories);
        }

        public ActionResult GridDeleteCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            return Delete(requestData, db.Categories, item => item.CategoryID);
        }

        public ActionResult GridUpdate([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Update(requestData, db.Customers);
        }

        public ActionResult GridCreate([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Create(requestData, db.Customers);
        }

        public ActionResult GridDelete([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Delete(requestData, db.Customers, item => item.CustomerID);
        }

        private ActionResult Update<T>(CollectionViewEditRequest<T> requestData, DbSet<T> data) where T : class
        {
            return this.C1Json(CollectionViewHelper.Edit<T>(requestData, item =>
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
                return new CollectionViewItemResult<T>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = item
                };
            }, () => data.ToList<T>()));
        }

        private ActionResult Create<T>(CollectionViewEditRequest<T> requestData, DbSet<T> data) where T : class
        {
            return this.C1Json(CollectionViewHelper.Edit<T>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    data.Add(item);
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
                return new CollectionViewItemResult<T>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = item
                };
            }, () => data.ToList<T>()));
        }

        private ActionResult Delete<T>(CollectionViewEditRequest<T> requestData, DbSet<T> data, Func<T, object> getKey) where T : class
        {
            return this.C1Json(CollectionViewHelper.Edit<T>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = data.Find(getKey(item));
                    data.Remove(resultItem);
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
                return new CollectionViewItemResult<T>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = item
                };
            }, () => data.ToList<T>()));
        }

    }
}
