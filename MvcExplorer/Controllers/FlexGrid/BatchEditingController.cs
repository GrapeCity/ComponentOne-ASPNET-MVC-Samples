using System.Data.Entity.Validation;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        //
        // GET: /BatchEditing/

        public ActionResult BatchEditing(CollectionViewRequest<Category> requestData)
        {
            return View(db.Categories.ToList());
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
                    var errorList = e.EntityValidationErrors.SelectMany(i => i.ValidationErrors).Select(i => i.ErrorMessage).ToList();

                    try
                    {
                        var refreshableObjects = db.ChangeTracker.Entries().Where(c => c.State == EntityState.Modified).ToList();
                        refreshableObjects.ForEach(o => o.Reload());
                    }
                    catch(Exception er)
                    {
                        errorList.Add(er.Message);
                    }

                    error = string.Join(",", errorList);
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
