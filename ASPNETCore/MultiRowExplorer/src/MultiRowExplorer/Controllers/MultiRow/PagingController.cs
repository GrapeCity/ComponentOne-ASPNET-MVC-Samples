using System.Collections.Generic;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MultiRowExplorer.Models;
using Microsoft.AspNetCore.Http;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions _pagingOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Page Size", new OptionItem {Values = new List<string> {"10", "25", "50", "100"}, CurrentValue = "10"}},
            }
        };

        public ActionResult Paging(IFormCollection data)
        {
            _pagingOptions.LoadPostData(data);
            ViewBag.DemoOptions = _pagingOptions;
            return View();
        }


        public ActionResult Paging_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }
    }
}
