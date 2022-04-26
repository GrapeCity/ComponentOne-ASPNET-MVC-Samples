using C1.Web.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class CollectionViewController : Controller
    {
        private readonly ControlOptions _collectionViewoptions = new ControlOptions
        {
            Options = new OptionDictionary
            {        
                //{"By Bapge", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}},
                {"Repeat Buttons", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                //{"HeaderFormat",new OptionItem{Values = new List<string> {"None", "Item: {current:n0} / {count:n0}", "Page: {currentPage} / {pageCount}"},CurrentValue = "None"}},
                {"Css Class", new OptionItem {Values = new List<string> {"None", "Red" , "Blue" , "Green"}, CurrentValue = "None"}},
            }
        };

        public ActionResult CollectionViewNavigator(FormCollection collection)
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

            _collectionViewoptions.LoadPostData(data);
            var model = Sale.GetData(500);
            ViewBag.DemoOptions = _collectionViewoptions;
            return View(model);
        }
    }
}