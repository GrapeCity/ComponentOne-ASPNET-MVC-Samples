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
        // GET: /ODataDeferCommits/
        private readonly ControlOptions _oDataDeferCommitsSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Defer Commits", new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "False"}}
            }
        };

        public ActionResult ODataDeferCommits(IFormCollection collection)
        {
            _oDataDeferCommitsSetting.LoadPostData(collection);
            ViewBag.DemoOptions = _oDataDeferCommitsSetting;
            return View();
        }
    }
}
