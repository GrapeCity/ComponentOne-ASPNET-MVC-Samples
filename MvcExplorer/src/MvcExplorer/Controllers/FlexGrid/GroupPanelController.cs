using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridGroupPanelModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Max Groups", new OptionItem {Values = new List<string> {"3", "4", "5", "6"}, CurrentValue = "3"}},
                {"Placeholder", new OptionItem {Values = new List<string> {Localization.FlexGridRes.GroupPanel_Placeholder1, Localization.FlexGridRes.GroupPanel_Placeholder2}, CurrentValue = Localization.FlexGridRes.GroupPanel_Placeholder1}},
                {"Hide Grouped Columns", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}},
                {"Group Description Creator", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}},
                {"Show Drag Glyphs", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}}
            }
        };

        public ActionResult GroupPanel(IFormCollection data)
        {
            _gridGroupPanelModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridGroupPanelModel;
            return View();
        }

        public ActionResult GroupPanel_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}
