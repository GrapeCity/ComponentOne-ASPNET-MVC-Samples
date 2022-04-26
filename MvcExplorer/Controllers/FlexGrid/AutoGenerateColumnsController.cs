using System.Web.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;
using System;
using C1.Web.Mvc;
using System.Collections;
using System.Linq;
using System.Globalization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridAutoGenerateColumnsOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Auto Generate Columns", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Number Columns Width", new OptionItem {Values = new List<string> {"60", "100", "120", "150"}, CurrentValue = "120"}},
                {"Date Columns Width", new OptionItem {Values = new List<string> {"60", "100", "120", "150"}, CurrentValue = "100"}},
                {"String Columns Width", new OptionItem {Values = new List<string> {"60", "100", "120", "150"}, CurrentValue = "150"}},
                {"Boolean Columns Width", new OptionItem {Values = new List<string> {"60", "100", "120", "150"}, CurrentValue = "60"}},
                {"Selection",new OptionItem{Values = new List<string> {"None", "Cell", "CellRange", "Row", "RowRange", "ListBox", "MultiRange"},CurrentValue = "Cell"}},
                {"Column Visibility",new OptionItem {Values = new List<string> {"Show", "Hide"}, CurrentValue = "Show"}},
                {"Copy Header", new OptionItem {Values = new List<string> {"None", "Column", "Row", "All"}, CurrentValue = "None"}},
                {"Lazy Render", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult AutoGenerateColumns(FormCollection collection)
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
            _gridAutoGenerateColumnsOptions.LoadPostData(data);
            ViewBag.DemoOptions = _gridAutoGenerateColumnsOptions;
            var model = Sale.GetData(500);
            return View(model);
        }
    }
}
