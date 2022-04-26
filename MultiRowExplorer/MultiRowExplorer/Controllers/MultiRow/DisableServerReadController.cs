using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1.Web.Mvc;
using System.Collections;
using System.Globalization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions _disableServerReadSetting = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Disable Server Read",new OptionItem{Values = new List<string> {"True", "False"},CurrentValue = "True"}}
            }
        };

        public ActionResult DisableServerRead(FormCollection collection)
        {
            _disableServerReadSetting.LoadPostData(collection);
            ViewBag.DemoOptions = _disableServerReadSetting;
            return View(Sale.GetData(500));
        }

    }
}
