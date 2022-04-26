using System.Data.Entity.Validation;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Collections;
using System.Globalization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        //
        // GET: /ODataDeferCommits/
        private readonly ControlOptions _oDataDeferCommitsSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Defer Commits", new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "False"}}
            }
        };

        public ActionResult ODataDeferCommits(FormCollection collection)
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

            _oDataDeferCommitsSetting.LoadPostData(data);
            ViewBag.DemoOptions = _oDataDeferCommitsSetting;
            return View();
        }
      
    }
}
