using System.Data.Entity.Validation;
using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        //
        // GET: /BatchEditing/

        public ActionResult BatchEditing(CollectionViewRequest<Supplier> requestData)
        {
            return View(db.Suppliers.ToList());
        }

        public ActionResult MultiRowBatchEdit([C1JsonRequest]CollectionViewBatchEditRequest<Supplier> requestData)
        {
            return this.C1Json(CollectionViewHelper.BatchEdit(requestData, batchData =>
            {
                var itemresults = new List<CollectionViewItemResult<Supplier>>();
                string error = string.Empty;
                bool success = true;
                try
                {
                    if (batchData.ItemsCreated != null)
                    {
                        batchData.ItemsCreated.ToList().ForEach(st =>
                        {
                            db.Suppliers.Add(st);
                            itemresults.Add(new CollectionViewItemResult<Supplier>
                            {
                                Error = "",
                                Success = ModelState.IsValid,
                                Data = st
                            });
                        });
                    }
                    if (batchData.ItemsDeleted != null)
                    {
                        batchData.ItemsDeleted.ToList().ForEach(supplier =>
                        {
                            var fSupplier = db.Suppliers.Find(supplier.SupplierID);
                            db.Suppliers.Remove(fSupplier);
                            itemresults.Add(new CollectionViewItemResult<Supplier>
                            {
                                Error = "",
                                Success = ModelState.IsValid,
                                Data = supplier
                            });
                        });
                    }
                    if (batchData.ItemsUpdated != null)
                    {
                        batchData.ItemsUpdated.ToList().ForEach(supplier =>
                        {
                            db.Entry(supplier).State = EntityState.Modified;
                            itemresults.Add(new CollectionViewItemResult<Supplier>
                            {
                                Error = "",
                                Success = ModelState.IsValid,
                                Data = supplier
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
                    catch (Exception er)
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

                return new CollectionViewResponse<Supplier>
                {
                    Error = error,
                    Success = success,
                    OperatedItemResults = itemresults
                };
            }, () => db.Suppliers.ToList()));
        }
    }
}
