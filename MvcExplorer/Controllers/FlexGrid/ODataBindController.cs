using C1.Web.Mvc;
using MvcExplorer.Models;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    partial class FlexGridController
    {
        private readonly ControlOptions _oDataBindSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Sort On Server", new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Filter On Server", new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult ODataBind(FormCollection collection)
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

            _oDataBindSetting.LoadPostData(data);
            ViewBag.DemoOptions = _oDataBindSetting;
            return View();
        }
    }
}