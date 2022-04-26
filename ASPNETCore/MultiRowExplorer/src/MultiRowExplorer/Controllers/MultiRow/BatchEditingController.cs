using System;
using System.Collections.Generic;
using System.Linq;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MultiRowExplorer.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        //
        // GET: /BatchEditing/

        public ActionResult BatchEditing()
        {
            return View();
        }

        public ActionResult BatchEditing_Bind([C1JsonRequest] CollectionViewRequest<Supplier> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Suppliers.ToList()));
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
                            _db.Suppliers.Add(st);
                            itemresults.Add(new CollectionViewItemResult<Supplier>
                            {
                                Error = "",
                                Success = success,
                                Data = st
                            });
                        });
                    }
                    if (batchData.ItemsDeleted != null)
                    {
                        batchData.ItemsDeleted.ToList().ForEach(supplier =>
                        {
                            _db.Suppliers.Remove(supplier);
                            itemresults.Add(new CollectionViewItemResult<Supplier>
                            {
                                Error = "",
                                Success = success,
                                Data = supplier
                            });
                        });
                    }
                    if (batchData.ItemsUpdated != null)
                    {
                        batchData.ItemsUpdated.ToList().ForEach(supplier =>
                        {
                            _db.Entry(supplier).State = EntityState.Modified;
                            itemresults.Add(new CollectionViewItemResult<Supplier>
                            {
                                Error = "",
                                Success = success,
                                Data = supplier
                            });
                        });
                    }
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    error = GetExceptionMessage(e);
                    success = false;
#if RELOAD_ON_ERROR
                    try
                    {
                        var refreshableObjects = _db.ChangeTracker.Entries().Where(c => c.State == EntityState.Modified).ToList();
                        refreshableObjects.ForEach(o => o.Reload());
                    }
                    catch (Exception er)
                    {
                        error = error + Environment.NewLine + er.Message;
                    }
#endif
                }

                return new CollectionViewResponse<Supplier>
                {
                    Error = error,
                    Success = success,
                    OperatedItemResults = itemresults
                };
            }, () => _db.Suppliers.ToList()));
        }

    }
}
