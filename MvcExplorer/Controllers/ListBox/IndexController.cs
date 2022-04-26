using MvcExplorer.Models;   
using System.Collections.Generic;  
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Case Sensitive Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}},
                {"Virtualization Threshold",new OptionItem{ Values = new List<string> { "Disable" , "0" }, CurrentValue = "Disable"}}
            }
        };
        public ActionResult Index(FormCollection collection)
        {                
            IValueProvider data = collection;  
            _optionModel.LoadPostData(data);
            ViewBag.DemoOptions = _optionModel;
            ViewBag.Cities = Cities.GetCities();
            return View();
        }
    }
}
