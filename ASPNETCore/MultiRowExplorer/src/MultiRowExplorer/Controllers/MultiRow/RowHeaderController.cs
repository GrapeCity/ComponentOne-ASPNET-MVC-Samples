using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MultiRowExplorer.Models;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions _rowHeaderOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Row Header",new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        // GET: GroupHeaders
        public ActionResult RowHeader(IFormCollection collection)
        {
            _rowHeaderOptions.LoadPostData(collection);
            ViewBag.RowHeaderOptions = _rowHeaderOptions;
            return View();
        }

        public ActionResult RowHeader_Read([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }
    }
}