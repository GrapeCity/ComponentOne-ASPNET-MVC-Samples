using MultiRowExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MultiRowExplorer.Localization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions groupPanelOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Max Groups", new OptionItem {Values = new List<string> {"3", "4", "5", "6"}, CurrentValue = "3"}},
                {"Placeholder", new OptionItem {Values = new List<string> { MultiRowRes.GroupPanel_Text1, MultiRowRes.GroupPanel_Text2}, CurrentValue = MultiRowRes.GroupPanel_Text1}}
            }
        };

        public ActionResult GroupPanel(IFormCollection data)
        {
            groupPanelOptions.LoadPostData(data);
            ViewBag.DemoOptions = groupPanelOptions;
            return View();
        }

        public ActionResult GroupPanel_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }
    }
}
