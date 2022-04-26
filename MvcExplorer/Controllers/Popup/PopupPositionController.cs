using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class PopupController : Controller
    {
        private readonly ControlOptions _gridDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Popup Position",new OptionItem{ Values = new List<string> {
                    "Above", "AboveRight", "RightTop", "Right", "RightBottom", "BelowRight", "Below", "BelowLeft",
                    "LeftBottom", "Left", "LeftTop", "AboveLeft"},
                    CurrentValue = "BelowLeft"}}
            }
        };
        public ActionResult PopupPosition(FormCollection collection)
        {
            
            IValueProvider data = collection;
            if (CallbackManager.CurrentIsCallback)
            {
                var request = CallbackManager.GetCurrentCallbackData<CollectionViewRequest<object>>();
                if (request != null && request.ExtraRequestData != null)
                {
                    var extraData = request.ExtraRequestData.Cast<DictionaryEntry>()
                        .ToDictionary(kvp => (string)kvp.Key, kvp => kvp.Value.ToString());
                    data = new DictionaryValueProvider<string>(extraData, CultureInfo.CurrentCulture);
                }
            }

            _gridDataModel.LoadPostData(data);                                                        
            ViewBag.DemoOptions = _gridDataModel;  
            return View();
        }
    }
}
