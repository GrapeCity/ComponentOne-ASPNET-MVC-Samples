using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class ComboBoxController : Controller
    {
        private readonly ControlOptions _options = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Case Sensitive Search",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "False"}},
                {"Handle Wheel",new OptionItem{ Values = new List<string> { "True", "False"}, CurrentValue = "True"}}
            }
        };
        private readonly C1NWindEntities _db;

        public ComboBoxController(C1NWindEntities db)
        {
            _db = db;
        }

        public ActionResult Index(IFormCollection collection)
        {
            _options.LoadPostData(collection);
            ViewBag.DemoOptions = _options;
            ViewBag.Countries = Countries.GetCountries();
            ViewBag.Cities = Cities.GetCities();
            return View();
        }
    }
}
