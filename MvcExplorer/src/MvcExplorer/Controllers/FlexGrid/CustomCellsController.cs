using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _customCellsDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Formatting", new OptionItem {Values = new List<string> {"On", "Off"}, CurrentValue = "Off"}},
                {"Css Class All", new OptionItem {Values = new List<string> {"None", "Red" , "Blue" , "Yellow"}, CurrentValue = "None"}}
            }
        };
        public ActionResult CustomCells(IFormCollection collection)
        {
            _customCellsDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _customCellsDataModel;
            return View();
        }

        public ActionResult CustomCells_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}
