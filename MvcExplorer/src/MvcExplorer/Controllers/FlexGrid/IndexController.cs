using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE30 && !NET50
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Items",new OptionItem{Values = new List<string> {"5", "50", "500", "5000", "50000", "100000", "500000", "1000000"},CurrentValue = "500"}},
                {"Allow Sorting", new OptionItem {Values = new List<string> {"None", "SingleColumn", "MultiColumn"}, CurrentValue = "None"}},
                {"Selection",new OptionItem{Values = new List<string> {"None", "Cell", "CellRange", "Row", "RowRange", "ListBox", "MultiRange"},CurrentValue = "Cell"}},
                {"Formatting", new OptionItem {Values = new List<string> {"On", "Off"}, CurrentValue = "Off"}},
                {"Column Visibility",new OptionItem {Values = new List<string> {"Show", "Hide"}, CurrentValue = "Show"}},
                {"Column Resize", new OptionItem {Values = new List<string> {"100", "150"}, CurrentValue = "100"}},
                {"Css Class All", new OptionItem {Values = new List<string> {"None", "Red" , "Blue" , "Yellow"}, CurrentValue = "None"}},
                {"Lazy Render", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Copy Header", new OptionItem {Values = new List<string> {"None", "Column", "Row", "All"}, CurrentValue = "None"}},
                {"Big Checkbox", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
            }
        };

        public ActionResult Index(IFormCollection collection)
        {
            return RedirectToAction("ShowCase");
            //_gridDataModel.LoadPostData(collection);
            //ViewBag.DemoOptions = _gridDataModel;
            //return View();
        }

        public ActionResult Index_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {            
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _gridDataModel.LoadPostData(data);
            var model = Sale.GetData(Convert.ToInt32(_gridDataModel.Options["items"].CurrentValue));
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}
