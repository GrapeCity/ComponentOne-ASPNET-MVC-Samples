using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectionView101.Models;
using C1.Web.Mvc.Grid;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace CollectionView101.Controllers
{
    public class HomeController : Controller
    {
        private C1NWindEntities db = new C1NWindEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db);
        }

        public ActionResult GridReadCategory([C1JsonRequest] CollectionViewRequest<Category> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, db.Categories));
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

        public ActionResult GridUpdateCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            return Update(requestData, db.Categories);
        }

        public ActionResult GridDeleteCategory([C1JsonRequest]CollectionViewEditRequest<Category> requestData)
        {
            return Delete(requestData, db.Categories, item => item.CategoryID);
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

        public ActionResult GridBatchEdit([C1JsonRequest]CollectionViewBatchEditRequest<Category> requestData)
        {
            return this.C1Json(CollectionViewHelper.BatchEdit(requestData, batchData =>
            {
                var itemresults = new List<CollectionViewItemResult<Category>>();
                string error = string.Empty;
                bool success = true;
                try
                {
                    if (batchData.ItemsCreated != null)
                    {
                        batchData.ItemsCreated.ToList().ForEach(st =>
                        {
                            db.Categories.Add(st);
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = ModelState.IsValid,
                                Data = st
                            });
                        });
                    }
                    if (batchData.ItemsDeleted != null)
                    {
                        batchData.ItemsDeleted.ToList().ForEach(category =>
                        {
                            var fCategory = db.Categories.Find(category.CategoryID);
                            db.Categories.Remove(fCategory);
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = ModelState.IsValid,
                                Data = category
                            });
                        });
                    }
                    if (batchData.ItemsUpdated != null)
                    {
                        batchData.ItemsUpdated.ToList().ForEach(category =>
                        {
                            db.Entry(category).State = EntityState.Modified;
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = ModelState.IsValid,
                                Data = category
                            });
                        });
                    }
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    error = string.Join(",", e.EntityValidationErrors.SelectMany(i => i.ValidationErrors).Select(i => i.ErrorMessage));
                    success = false;
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }

                return new CollectionViewResponse<Category>
                {
                    Error = error,
                    Success = success,
                    OperatedItemResults = itemresults
                };
            }, () => db.Categories.ToList()));
        }
    }
}