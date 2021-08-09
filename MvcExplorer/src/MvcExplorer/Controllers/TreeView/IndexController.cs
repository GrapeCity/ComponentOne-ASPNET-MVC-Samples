using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class TreeViewController : Controller
    {
        // GET: Index
        public ActionResult Index(IFormCollection collection)
        {
            _treeViewDataModel.LoadPostData(collection);
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