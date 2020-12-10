using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _oDataBindSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Sort On Server", new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                {"Filter On Server", new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult ODataBind(IFormCollection collection)
        {
            _oDataBindSetting.LoadPostData(collection);
            ViewBag.DemoOptions = _oDataBindSetting;
            // NETCORE 3.0 doesn't not fully support ODataServer yet, so the local source is not working.
#if ODATA_SERVER && !NETCORE30 && !NET50
            ViewBag.IsReadOnly = false;
#else
            ViewBag.IsReadOnly = true;
#endif
            return View();
        }
    }
}
