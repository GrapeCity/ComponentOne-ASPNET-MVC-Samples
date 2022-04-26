using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc;
using System.Collections;
using System.Globalization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _disableServerReadSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Disable Server Read",new OptionItem{Values = new List<string> {"True", "False"},CurrentValue = "True"}}
            }
        };

        public ActionResult DisableServerRead(FormCollection collection)
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

            _disableServerReadSetting.LoadPostData(data);
            ViewBag.DemoOptions = _disableServerReadSetting;
            return View(Sale.GetData(500));
        }

    }
}
