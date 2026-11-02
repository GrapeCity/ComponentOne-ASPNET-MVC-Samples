using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _allowResizingDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"AllowResizing",
                    new OptionItem
                    {
                        Values = Enum.GetNames(typeof(C1.Web.Mvc.Grid.AllowResizing)).ToList()
                        , CurrentValue = C1.Web.Mvc.Grid.AllowResizing.None.ToString()
                    }
                }
            }
        };

        public ActionResult AllowResizing(FormCollection data)
        {
            _allowResizingDataModel.LoadPostData(data);
            var model = Sale.GetData(50);
            ViewBag.DemoOptions = _allowResizingDataModel;
            return View(model);
        }
    }
}
