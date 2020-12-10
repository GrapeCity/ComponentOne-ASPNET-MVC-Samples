using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace RazorPagesExplorer.Pages
{
    public class UnobtrusiveValidationModel : PageModel
    {
        public static readonly List<UserInfo> Users = UsersData.Users;

        public void OnGet()
        {
        }

        public ActionResult OnPostGridUpdateUserInfo([C1JsonRequest]CollectionViewEditRequest<UserInfo> requestData)
        {
            return JsonConvertHelper.C1Json(CollectionViewHelper.Edit<UserInfo>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = Users.Find(u => u.Id == item.Id);
                    var index = Users.IndexOf(resultItem);
                    Users.Remove(resultItem);
                    Users.Insert(index, item);
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
            }, () => Users));
        }

        public ActionResult OnPostGridCreateUserInfo([C1JsonRequest]CollectionViewEditRequest<UserInfo> requestData)
        {
            return JsonConvertHelper.C1Json(CollectionViewHelper.Edit<UserInfo>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    Users.Add(item);
                    item.Id = Users.Max(u => u.Id) + 1;
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
            }, () => Users));
        }

        public ActionResult OnPostGridDeleteUserInfo([C1JsonRequest]CollectionViewEditRequest<UserInfo> requestData)
        {
            return JsonConvertHelper.C1Json(CollectionViewHelper.Edit<UserInfo>(requestData, item =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var resultItem = Users.Find(u => u.Id == item.Id);
                    Users.Remove(resultItem);
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
            }, () => Users));
        }
    }
}