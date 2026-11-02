using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult AllowResizing(IFormCollection collection)
        {
            _allowResizingDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _allowResizingDataModel;
            return View();
        }

        public ActionResult AllowResizing_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(50)));
        }
    }
}
