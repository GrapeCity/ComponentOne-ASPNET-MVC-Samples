using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DateTimeFields.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DateTimeFields.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region for defaultGrid
        private static List<DatesData> defaultData = DatesData.GetData(3).ToList();
        public ActionResult ReadDatesData([C1JsonRequest] CollectionViewRequest<DatesData> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, defaultData));
        }

        public ActionResult UpdateDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            return Update(requestData, defaultData);
        }

        public ActionResult CreateDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            return Create(requestData, defaultData);
        }

        public ActionResult DeleteDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            return Delete(requestData, defaultData);
        }
        #endregion for defaultGrid

        #region for convertedGrid
        private static List<DatesData> convertedData = DatesData.GetData(3).ToList();
        private static List<DatesData> ConvertToUnspecifiedData(IEnumerable<DatesData> sourceData)
        {
            return sourceData.Select(item => new DatesData
            {
                Id = item.Id,
                UnspecifiedDateTime = item.UnspecifiedDateTime,
                UtcDateTime = new DateTime(item.UtcDateTime.Ticks),
                LocalDateTime = new DateTime(item.LocalDateTime.Ticks)
            }).ToList();
        }

        private static void ConvertUpspecifiedBack(CollectionViewEditRequest<DatesData> requestData)
        {
            // Convert Unspecified DateTime back.
            foreach (var item in requestData.OperatingItems)
            {
                item.LocalDateTime = new DateTime(item.LocalDateTime.Ticks, DateTimeKind.Local);
                item.UtcDateTime = new DateTime(item.UtcDateTime.Ticks, DateTimeKind.Utc);
            }
        }
        public ActionResult Converted_ReadDatesData([C1JsonRequest] CollectionViewRequest<DatesData> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, ConvertToUnspecifiedData(convertedData)));
        }
        public ActionResult Converted_UpdateDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            ConvertUpspecifiedBack(requestData);
            return Update(requestData, convertedData, ConvertToUnspecifiedData);
        }
        public ActionResult Converted_CreateDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            ConvertUpspecifiedBack(requestData);
            return Create(requestData, convertedData, ConvertToUnspecifiedData);
        }
        public ActionResult Converted_DeleteDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            ConvertUpspecifiedBack(requestData);
            return Delete(requestData, convertedData, ConvertToUnspecifiedData);
        }
        #endregion for convertedGrid

        #region for customGrid
        private static List<DatesData> customData = DatesData.GetData(3).ToList();
        public ActionResult Custom_ReadDatesData([C1JsonRequest] CollectionViewRequest<DatesData> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, customData));
        }

        public ActionResult Custom_UpdateDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            return Update(requestData, customData);
        }

        public ActionResult Custom_CreateDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            return Create(requestData, customData);
        }

        public ActionResult Custom_DeleteDatesData([C1JsonRequest]CollectionViewEditRequest<DatesData> requestData)
        {
            return Delete(requestData, customData);
        }
        #endregion for customGrid

        #region CRUD methods
        public ActionResult Update(CollectionViewEditRequest<DatesData> requestData, List<DatesData> sourceData, Func<IEnumerable<DatesData>, List<DatesData>> converter = null)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                var error = string.Empty;
                var success = true;
                try
                {
                    var index = sourceData.FindIndex(u => u.Id == item.Id);
                    sourceData.RemoveAt(index);
                    sourceData.Insert(index, item);
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<DatesData>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => converter != null ? converter(sourceData) : sourceData));
        }

        public ActionResult Create(CollectionViewEditRequest<DatesData> requestData, List<DatesData> sourceData, Func<IEnumerable<DatesData>, List<DatesData>> converter = null)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                var error = string.Empty;
                var success = true;
                try
                {
                    sourceData.Add(item);
                    item.Id = sourceData.Max(u => u.Id) + 1;
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<DatesData>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => converter != null ? converter(sourceData) : sourceData));
        }

        public ActionResult Delete(CollectionViewEditRequest<DatesData> requestData, List<DatesData> sourceData, Func<IEnumerable<DatesData>, List<DatesData>> converter = null)
        {
            return this.C1Json(CollectionViewHelper.Edit(requestData, item =>
            {
                var error = string.Empty;
                var success = true;
                try
                {
                    var index = sourceData.FindIndex(u => u.Id == item.Id);
                    sourceData.RemoveAt(index);
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<DatesData>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => converter != null ? converter(sourceData) : sourceData));
        }
        #endregion CRUD methods
    }
}