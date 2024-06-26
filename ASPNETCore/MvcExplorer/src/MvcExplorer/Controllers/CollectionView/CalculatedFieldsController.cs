﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class CollectionViewController : Controller
    {
        public ActionResult CalculatedFields(IFormCollection collection)
        {
            return View();
        }

        public ActionResult CalculatedFields_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _optionsModel.LoadPostData(data);
            var model = Sale.GetData(500);
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }

    }
}