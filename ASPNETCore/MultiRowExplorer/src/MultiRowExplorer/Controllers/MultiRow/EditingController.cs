using C1.Web.Mvc;
using System;
using System.Linq;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MultiRowExplorer.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly C1NWindEntities _db;

        public MultiRowController(C1NWindEntities db)
        {
            _db = db;
        }

        public ActionResult Editing()
        {
            return View();
        }

        public ActionResult MultiRowBindSupplier([C1JsonRequest] CollectionViewRequest<Supplier> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Suppliers.ToList()));
        }

        public ActionResult MultiRowUpdateSupplier([C1JsonRequest]CollectionViewEditRequest<Supplier> requestData)
        {
            return Update(requestData, _db.Suppliers);
        }

        public ActionResult MultiRowCreateSupplier([C1JsonRequest]CollectionViewEditRequest<Supplier> requestData)
        {
            var supplier = requestData.OperatingItems.First();
            if (supplier.CompanyName == null)
            {
                supplier.CompanyName = "";
            }

            return Create(requestData, _db.Suppliers);
        }

        public ActionResult MultiRowDeleteSupplier([C1JsonRequest]CollectionViewEditRequest<Supplier> requestData)
        {
            return Delete(requestData, _db.Suppliers, item => item.SupplierID);
        }


        public ActionResult MultiRowBindCustomer([C1JsonRequest] CollectionViewRequest<Customer> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Customers.ToList()));
        }

        public ActionResult MultiRowUpdateCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Update(requestData, _db.Customers);
        }

        public ActionResult MultiRowCreateCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Create(requestData, _db.Customers);
        }

        public ActionResult MultiRowDeleteCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Delete(requestData, _db.Customers, item => item.CustomerID);
        }

        private ActionResult Update<T>(CollectionViewEditRequest<T> requestData, DbSet<T> data) where T : class
        {
            return this.C1Json(CollectionViewHelper.Edit<T>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    _db.Entry(item as object).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    error = GetExceptionMessage(e);
                    success = false;
                }
                return new CollectionViewItemResult<T>
                {
                    Error = error,
                    Success = success,
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
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    error = GetExceptionMessage(e);
                    success = false;
                }
                return new CollectionViewItemResult<T>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => data.ToList<T>()));
        }

        private ActionResult Delete<T>(CollectionViewEditRequest<T> requestData, DbSet<T> data, Func<T, object> getKey) where T : class
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    T resultItem = null;
                    foreach (var i in data)
                    {
                        if (string.Equals(getKey(i).ToString(), getKey(item).ToString()))
                        {
                            resultItem = i;
                            break;
                        }
                    }

                    if (resultItem != null)
                    {
                        data.Remove(resultItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    error = GetExceptionMessage(e);
                    success = false;
                }
                return new CollectionViewItemResult<T>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => data.ToList()));
        }

        /// <summary>
        /// In order to get the real exception message.
        /// </summary>
        private static SqlException GetSqlException(Exception e)
        {
            while (e != null && !(e is SqlException))
            {
                e = e.InnerException;
            }
            return e as SqlException;
        }

        // Get the real exception message
        internal static string GetExceptionMessage(Exception e)
        {
            var msg = e.Message;
            var sqlException = GetSqlException(e);
            if (sqlException != null)
            {
                msg = sqlException.Message;
            }
            return msg;
        }
    }
}
