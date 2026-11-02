using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class CollectionViewController : Controller
    {
        private const int NewItemIndexPageSize = 6;

        private readonly ControlOptions _cVNewItemIndex = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"NewItemIndex", new OptionItem {Values = new List<string> { "Default", "Top", "Bottom" }, CurrentValue = "Default"}}
            }
        };

        public ActionResult NewItemIndex(IFormCollection collection)
        {
            _cVNewItemIndex.LoadPostData(collection);
            ViewBag.DemoOptions = _cVNewItemIndex;
            ViewBag.PageSize = NewItemIndexPageSize;

            // "Top" puts the new row at the first position of the current page and
            // "Bottom" at the last one. "Default" (-1) keeps the original AddNew
            // behavior, which appends to the end of the source collection.
            switch (_cVNewItemIndex.Options["NewItemIndex"].CurrentValue)
            {
                case "Top":
                    ViewBag.NewItemIndex = 0;
                    break;
                case "Bottom":
                    ViewBag.NewItemIndex = NewItemIndexPageSize - 1;
                    break;
                default:
                    ViewBag.NewItemIndex = -1;
                    break;
            }

            return View(Sale.GetData(20).ToList());
        }
    }
}
