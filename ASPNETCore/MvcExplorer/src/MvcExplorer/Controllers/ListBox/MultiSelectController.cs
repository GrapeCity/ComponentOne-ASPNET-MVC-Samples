using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        private readonly C1NWindEntities _db;

        public ListBoxController(C1NWindEntities db)
        {
            _db = db;
        }
        public ActionResult MultiSelect()
        {
            return View(_db);
        }

        public ActionResult ListUpdateProducts([C1JsonRequest]CollectionViewEditRequest<Product> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Product>(requestData, item =>
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
                return new CollectionViewItemResult<Product>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => _db.Products.ToList<Product>()));
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

        public ActionResult UpdateCheckedItems([FromBody] dynamic data)
        {
            try
            {
                // Extract checkedItems from the incoming JSON object
                var checkedItems = data.checkedItems.ToObject<List<int>>();
                // Update the database based on the checked items
                var products = _db.Products.ToList();
                foreach (var product in products)
                {
                    product.Discontinued = checkedItems.Contains(product.ProductID);
                }
                _db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
