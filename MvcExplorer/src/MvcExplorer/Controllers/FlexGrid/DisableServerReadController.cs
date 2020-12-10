using System.Collections.Generic;
using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _disableServerReadSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Disable Server Read",new OptionItem{Values = new List<string> {"True", "False"},CurrentValue = "True"}}
            }
        };

        public ActionResult DisableServerRead(IFormCollection collection)
        {
            _disableServerReadSetting.LoadPostData(collection);
            ViewBag.DemoOptions = _disableServerReadSetting;
            return View();
        }

        public ActionResult DisableServerRead_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }

    }
}
