using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class PopupController : Controller
    {
        private readonly ControlOptions _gridDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Popup Position",new OptionItem{ Values = new List<string> {
                    "Above", "AboveRight", "RightTop", "Right", "RightBottom", "BelowRight", "Below", "BelowLeft",
                    "LeftBottom", "Left", "LeftTop", "AboveLeft"},
                    CurrentValue = "BelowLeft"}}
            }
        };
        public ActionResult PopupPosition(IFormCollection collection)
        {
            _gridDataModel.LoadPostData(collection);
            ViewBag.DemoOptions = _gridDataModel;
            return View();
        }
    }
}
