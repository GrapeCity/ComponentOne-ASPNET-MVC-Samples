using System;
using System.Collections.Generic;
using System.Linq;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        //
        // GET: /BatchEditing/

        public ActionResult BatchEditing()
        {
            return View();
        }

        public ActionResult BatchEditing_Bind([C1JsonRequest] CollectionViewRequest<Category> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Categories.ToList()));
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
                            _db.Categories.Add(st);
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = success,
                                Data = st
                            });
                        });
                    }
                    if (batchData.ItemsDeleted != null)
                    {
                        batchData.ItemsDeleted.ToList().ForEach(category =>
                        {
                            _db.Categories.Remove(category);
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = success,
                                Data = category
                            });
                        });
                    }
                    if (batchData.ItemsUpdated != null)
                    {
                        batchData.ItemsUpdated.ToList().ForEach(category =>
                        {
                            _db.Entry(category).State = EntityState.Modified;
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = success,
                                Data = category
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

                return new CollectionViewResponse<Category>
                {
                    Error = error,
                    Success = success,
                    OperatedItemResults = itemresults
                };
            }, () => _db.Categories.ToList()));
        }

    }
}
