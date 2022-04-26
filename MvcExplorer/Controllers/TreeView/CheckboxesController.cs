using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TreeViewController : Controller
    {
        // GET: Checkboxes
        public ActionResult Checkboxes(FormCollection collection)
        {       
            IValueProvider data = collection;
            _treeViewCheckboxesDataModel.LoadPostData(data);
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