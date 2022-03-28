using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using System.Collections.Generic;

namespace MvcExplorer.Controllers
{
    public partial class TabOrderController : Controller
    {
        private readonly ControlOptions _optionModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Tab Order",new OptionItem{ Values = new List<string> { "Default", "Customized"}, CurrentValue = "Default"}}
            }
        };
        public ActionResult Index(IFormCollection collection)
        {
            _optionModel.LoadPostData(collection);
            ViewBag.DemoOptions = _optionModel;
            return View();
        }

        public ActionResult GetCustomerOrder([C1JsonRequest] CollectionViewRequest<CustomerOrder> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, CustomerOrder.GetCountryGroupOrderData()));
        }

        public ActionResult GetCustomerSale([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, CustomerSale.GetData(10)));
        }
    }
}
