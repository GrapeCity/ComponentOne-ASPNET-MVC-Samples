using C1.Web.Mvc;
using System;
using System.Linq;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly C1NWindEntities _db;

        public FlexGridController(C1NWindEntities db)
        {
            _db = db;
        }

        public ActionResult Editing()
        {
            return View();
        }

        public ActionResult GridBindCategory([C1JsonRequest] CollectionViewRequest<Category> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Categories.ToList()));
        }

        public ActionResult GridUpdateCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            return Update(requestData, _db.Categories);
        }

        public ActionResult GridCreateCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            var category = requestData.OperatingItems.First();
            if (category.CategoryName == null)
            {
                category.CategoryName = "";
            }

            return Create(requestData, _db.Categories);
        }

        public ActionResult GridDeleteCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            return Delete(requestData, _db.Categories, item => item.CategoryID);
        }


        public ActionResult GridBindCustomer([C1JsonRequest] CollectionViewRequest<Customer> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Customers.ToList()));
        }

        public ActionResult GridUpdateCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Update(requestData, _db.Customers);
        }

        public ActionResult GridCreateCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
        {
            return Create(requestData, _db.Customers);
        }

        public ActionResult GridDeleteCustomer([C1JsonRequest]CollectionViewEditRequest<Customer> requestData)
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
                        if(string.Equals(getKey(i).ToString(), getKey(item).ToString()))
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
            while(e != null && !(e is SqlException))
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
