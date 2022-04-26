using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesExplorer.Models;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;
using System.Linq;
using Microsoft.Extensions.Primitives;
using System;

namespace RazorPagesExplorer.Pages
{
    public class FlexGridModel : PageModel
    {
        private readonly ControlOptions _gridDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Items",new OptionItem{Values = new List<string> {"5", "50", "500", "5000", "50000", "100000", "500000", "1000000"},CurrentValue = "500"}},
                {"Allow Sorting", new OptionItem {Values = new List<string> {"None", "SingleColumn","MultiColumn"}, CurrentValue = "None"}},
                {"Selection",new OptionItem{Values = new List<string> {"None", "Cell", "CellRange", "Row", "RowRange", "ListBox","MultiRange"},CurrentValue = "Cell"}},
                {"Formatting", new OptionItem {Values = new List<string> {"On", "Off"}, CurrentValue = "Off"}},
                {"Column Visibility",new OptionItem {Values = new List<string> {"Show", "Hide"}, CurrentValue = "Show"}},
                {"Column Resize", new OptionItem {Values = new List<string> {"100", "150"}, CurrentValue = "100"}}
            }
        };

        private void SetDemoOptions(IFormCollection collection)
        {
            _gridDataModel.LoadPostData(collection);
            ViewData["DemoOptions"] = _gridDataModel;
        }

        public void OnGet(IFormCollection collection)
        {
            SetDemoOptions(collection);
        }

        public void OnPost(IFormCollection collection)
        {
            SetDemoOptions(collection);
        }

        public ActionResult OnPostBind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _gridDataModel.LoadPostData(data);
            var model = Sale.GetData(Convert.ToInt32(_gridDataModel.Options["items"].CurrentValue));
            return JsonConvertHelper.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}