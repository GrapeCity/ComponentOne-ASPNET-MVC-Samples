using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers.CollectionView
{
    public partial class CollectionViewController : Controller
    {
        private readonly ControlOptions _collectionViewoptions = new ControlOptions
        {
            Options = new OptionDictionary
            {        
                //{"By Bapge", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}},
                {"Repeat Buttons", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "True"}},
                //{"HeaderFormat",new OptionItem{Values = new List<string> {"None", "Item: {current:n0} / {count:n0}", "Page: {currentPage} / {pageCount}"},CurrentValue = "None"}},
                {"Css Class", new OptionItem {Values = new List<string> {"None", "Red" , "Blue" , "Green"}, CurrentValue = "None"}},
            }
        };

        public ActionResult CollectionViewNavigator(IFormCollection collection)
        {
            _collectionViewoptions.LoadPostData(collection);
            ViewBag.DemoOptions = _collectionViewoptions;
            return View();
        }

        public ActionResult CollectionViewNavigator_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var extraData = requestData.ExtraRequestData
                 .ToDictionary(kvp => kvp.Key, kvp => new StringValues(kvp.Value.ToString()));
            var data = new FormCollection(extraData);
            _collectionViewoptions.LoadPostData(data);
            var model = Sale.GetData(500);
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}