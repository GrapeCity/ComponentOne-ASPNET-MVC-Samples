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
using C1.Web.Mvc.Grid;

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

        public ActionResult AutoGenerateColumns(IFormCollection data)
        {
            _gridAutoGenerateColumnsOptions.LoadPostData(data);
            ViewBag.DemoOptions = _gridAutoGenerateColumnsOptions;
            var opts = _gridAutoGenerateColumnsOptions.Options;
            ViewBag.DefTypeWidth = String.Format("{0},{1},", DataType.Number, opts["Number Columns Width"].CurrentValue)
                                   + String.Format("{0},{1},", DataType.Date, opts["Date Columns Width"].CurrentValue)
                                   + String.Format("{0},{1},", DataType.String, opts["String Columns Width"].CurrentValue)
                                   + String.Format("{0},{1},", DataType.Boolean, opts["Boolean Columns Width"].CurrentValue);
            var model = Sale.GetData(500);
            return View(model);
        }
    }
}