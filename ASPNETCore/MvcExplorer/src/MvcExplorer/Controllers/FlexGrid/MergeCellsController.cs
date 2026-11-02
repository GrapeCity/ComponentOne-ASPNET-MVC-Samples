using C1.Web.Mvc;
using C1.Web.Mvc.Grid;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _mergeCellOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"AllowMerging", new OptionItem{Values = new List<string> {"All", "None", "Cells", "ColumnHeaders", "RowHeaders", "AllHeaders"}, CurrentValue = "All"}},
                {"ExpandSelectionOnCopyPaste", new OptionItem{Values = new List<string>{"false", "true" }, CurrentValue="false" } },
                {"SkipMerged", new OptionItem{Values = new List<string>{"false", "true" }, CurrentValue="false" } },
                {"PasteEmptyCells", new OptionItem{Values = new List<string>{"true", "false" }, CurrentValue="false" } }
            }
        };
        private AllowMerging GetAllowMerging(ControlOptions controlOptions)
        {
            var allowMerging = controlOptions.Options["AllowMerging"].CurrentValue;
            return (AllowMerging)Enum.Parse(typeof(AllowMerging), allowMerging);
        }

        public ActionResult MergeCells(IFormCollection data)
        {
            _mergeCellOptions.LoadPostData(data);
            ViewBag.DemoOptions = _mergeCellOptions;
            ViewBag.AllowMerging = GetAllowMerging(_mergeCellOptions);
            return View();
        }

        public ActionResult MergeCells_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}
