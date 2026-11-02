using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static List<Sale> _clipboardSource = Sale.GetData(20).ToList();

        //
        // GET: /Clipboard/

        public ActionResult Clipboard()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"AutoClipboard", new object[]{"true", "false"}},
                    {"PreventCut", new object[]{"true", "false"}}
                }
            };

            return View(_clipboardSource);
        }
    }
}
