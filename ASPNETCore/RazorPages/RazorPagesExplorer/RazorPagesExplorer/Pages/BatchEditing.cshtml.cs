using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using RazorPagesExplorer.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace RazorPagesExplorer.Pages
{
    public class BatchEditingModel : PageModel
    {
        private static List<Category> _categories = Category.GetCategories();

        public BatchEditingModel()
        {
        }

        public void OnGet()
        {
        }

        public ActionResult OnPostBatchEditing_Bind([C1JsonRequest] CollectionViewRequest<Category> requestData)
        {
            return JsonConvertHelper.C1Json(CollectionViewHelper.Read(requestData, _categories));
        }

        public ActionResult OnPostGridBatchEdit([C1JsonRequest]CollectionViewBatchEditRequest<Category> requestData)
        {
            return JsonConvertHelper.C1Json(CollectionViewHelper.BatchEdit(requestData, batchData =>
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
                            st.CategoryID = _categories.Count == 0 ? 1 : _categories.Max(c => c.CategoryID) + 1;
                            _categories.Add(st);
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
                            var removeItemIndex = _categories.FindIndex(c => c.CategoryID == category.CategoryID);
                            _categories.RemoveAt(removeItemIndex);
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
                            var updateItem = _categories.Find(c => c.CategoryID == category.CategoryID);
                            updateItem.CategoryName = category.CategoryName;
                            updateItem.Description = category.Description;
                            itemresults.Add(new CollectionViewItemResult<Category>
                            {
                                Error = "",
                                Success = success,
                                Data = category
                            });
                        });
                    }
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
            }, () => _categories));
        }
    }
}