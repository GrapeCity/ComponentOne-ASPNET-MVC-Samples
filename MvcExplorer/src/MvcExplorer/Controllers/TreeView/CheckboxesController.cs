using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class TreeViewController : Controller
    {
        // GET: Checkboxes
        public ActionResult Checkboxes(IFormCollection collection)
        {
            _treeViewCheckboxesDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _treeViewCheckboxesDataModel;
            return View(Property.GetData(Url));
        }

        private readonly ControlOptions _treeViewCheckboxesDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"CheckOnClick",new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "False"}},
                {"CheckedMemberPath", new OptionItem{Values = new List<string> { "null", "NewItem"},CurrentValue = "null"}}
            }
        };
    }
}