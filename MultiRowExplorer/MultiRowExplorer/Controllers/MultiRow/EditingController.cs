using C1.Web.Mvc;
using MultiRowExplorer.Models;
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

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private C1NWindContext db = new C1NWindContext();
        //
        // GET: /Editing/

        public ActionResult Editing()
        {
            return View(db);
        }

        public ActionResult MultiRowUpdateSupplier([C1JsonRequest]CollectionViewEditRequest<Supplier> requestData)
        {
            return Update(requestData, db.Suppliers);
        }

        public ActionResult MultiRowCreateSupplier([C1JsonRequest]CollectionViewEditRequest<Supplier> requestData)
        {
            var supplier = requestData.OperatingItems.First();
            if (supplier.CompanyName == null)
            {
                supplier.CompanyName = "";
            }

            return Create(requestData, db.Suppliers);
        }

        public ActionResult MultiRowDeleteSupplier([C1JsonRequest]CollectionViewEditRequest<Supplier> requestData)
        {
            return Delete(requestData, db.Suppliers, item => item.SupplierID);
        }

        public ActionResult MultiRowUpdateCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Update(requestData, db.Customers);
        }

        public ActionResult MultiRowCreateCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Create(requestData, db.Customers);
        }

        public ActionResult MultiRowDeleteCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
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
