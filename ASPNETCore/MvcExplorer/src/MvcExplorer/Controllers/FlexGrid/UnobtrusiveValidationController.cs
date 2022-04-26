using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<UserInfo> users = UsersData.Users;

        public ActionResult UnobtrusiveValidation()
        {
            return View(users);
        }

        public ActionResult GridUpdateUserInfo([C1JsonRequest]CollectionViewEditRequest<UserInfo> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<UserInfo>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = users.Find(u => u.Id == item.Id);
                    var index = users.IndexOf(resultItem);
                    users.Remove(resultItem);
                    users.Insert(index, item);
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<UserInfo>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => users));
        }

        public ActionResult GridCreateUserInfo([C1JsonRequest]CollectionViewEditRequest<UserInfo> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<UserInfo>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    users.Add(item);
                    item.Id = users.Max(u => u.Id) + 1;
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<UserInfo>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => users));
        }

        public ActionResult GridDeleteUserInfo([C1JsonRequest]CollectionViewEditRequest<UserInfo> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<UserInfo>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = users.Find(u => u.Id == item.Id);
                    users.Remove(resultItem);
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<UserInfo>
                {
                    Error = error,
                    Success = success,
                    Data = item
                };
            }, () => users));
        }
    }
}