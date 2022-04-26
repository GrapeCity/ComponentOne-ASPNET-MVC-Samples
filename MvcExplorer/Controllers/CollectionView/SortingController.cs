using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class CollectionViewController : Controller
    {
        private readonly ControlOptions _cVSorting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"SortNulls", new OptionItem {Values = new List<string> { "Natural", "First", "Last" }, CurrentValue = "Natural"}}
            }
        };

        public ActionResult Sorting(FormCollection data)
        {
            _cVSorting.LoadPostData(data);
            ViewBag.DemoOptions = _cVSorting;
            var model = Sale.GetData(10).ToList();
            var nullObj = new Sale { };
            model.Add(nullObj);
            return View(model);
        }
    }
}