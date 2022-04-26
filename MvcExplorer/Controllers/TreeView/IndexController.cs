using MvcExplorer.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TreeViewController : Controller
    {
        // GET: Index
        public ActionResult Index(FormCollection collection)
        {
            IValueProvider data = collection;
            _treeViewDataModel.LoadPostData(data);
            ViewBag.DemoOptions = _treeViewDataModel;
            return View(Property.GetData(Url));
        }

        private readonly ControlOptions _treeViewDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"IsAnimated",new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "True"}},
                {"AutoCollapse", new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "True"}},
                {"ExpandOnClick",new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "True"}},
                {"CollapseOnClick",new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "False"}},
                {"ExpandOnLoad",new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "True"}},
                {"CollapseWhenDisabled",new OptionItem{Values = new List<string> { "True", "False"},CurrentValue = "True"}}
            }
        };
    }
}