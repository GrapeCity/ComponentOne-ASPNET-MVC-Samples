using System.Collections.Generic;  
using System.Web.Mvc;            
using MvcExplorer.Models;      

namespace MvcExplorer.Controllers
{
    public partial class AutoCompleteController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Case Sensitive Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}},
                {"Begins With Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}}
            }
        };
        public ActionResult Index (FormCollection collection)
        {            
            IValueProvider data = collection; 
            _optionModel.LoadPostData(data);
            ViewBag.DemoOptions = _optionModel;   
            ViewBag.Countries = Countries.GetCountries();
            return View();
        }
    }
}
