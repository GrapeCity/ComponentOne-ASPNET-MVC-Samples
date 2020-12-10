using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridSearchOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Delay",new OptionItem{Values = new List<string> {"100", "300", "500", "800", "1000"}, CurrentValue = "500"}},
                {"Css Match", new OptionItem {Values = new List<string> {"Default", "color-match", "underline-match", "style-match"}, CurrentValue = "Default"}},
                {"Case Sensitive Search", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult Searching(IFormCollection data)
        {
            _gridSearchOptions.LoadPostData(data);
            ViewBag.DemoOptions = _gridSearchOptions;
            return View();
        }

        public ActionResult Searching_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(200)));
        }
    }
}
