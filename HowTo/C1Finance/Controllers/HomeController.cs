using C1.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C1Finance.Models;
using C1.Web.Mvc.Serialization;

namespace C1Finance.Controllers
{
    public class HomeController : Controller
    {
        //Controller for Index.cshtml
        public ActionResult Index()
        {
            if (CallbackManager.CurrentIsCallback)
            {
                var request = CallbackManager.GetCurrentCallbackData<CollectionViewRequest<object>>();
                if (request != null && request.ExtraRequestData != null)
                {
                    const string stockNamesKey = "stockNames";
                    if (request.ExtraRequestData.ContainsKey(stockNamesKey))
                    {
                        var stockNameString = request.ExtraRequestData[stockNamesKey] as string;
                        if (!string.IsNullOrEmpty(stockNameString))
                        {
                            var stockNames = stockNameString.Split(',').ToList();
                            string SymbolToAdd = stockNames[stockNames.Count - 1];
                            PortfolioStatic.AddToPortfolioFGridList(SymbolToAdd);
                        }
                    }
                }
            }
            //Creating object of Portfolio Class
            PortfolioModel PModel1 = new PortfolioModel();
            return View(PModel1);
        }

        //To Delete selected portfolio
        public ActionResult GridDelete([C1JsonRequest]CollectionViewEditRequest<PortfolioStatic.PortfolioFGridClass> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<PortfolioStatic.PortfolioFGridClass>(requestData, customer =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var result = PortfolioStatic.PortfolioListFGrid.FirstOrDefault(item => item.Symbol == customer.Symbol);
                    if (result != null)
                    {
                        PortfolioStatic.PortfolioListFGrid.Remove(result);
                    }
                    else
                    {
                        error = "Cannot find the item.";
                        success = false;
                    }
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<PortfolioStatic.PortfolioFGridClass>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = customer
                };
            }, () => PortfolioStatic.PortfolioListFGrid.ToList<PortfolioStatic.PortfolioFGridClass>()));
        }

        //To Update selected portfolio
        public ActionResult GridUpdate([C1JsonRequest]CollectionViewEditRequest<PortfolioStatic.PortfolioFGridClass> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<PortfolioStatic.PortfolioFGridClass>(requestData, customer =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    PortfolioStatic.PortfolioListFGrid.Where(x => x.Symbol == customer.Symbol).ToList().ForEach(x =>
                    {
                        x.Change = customer.Change;
                        x.Chart = customer.Chart;
                        x.Color = customer.Color;
                        x.Cost = customer.Shares * customer.Price;
                        x.Gain = customer.Price > 0 ? (customer.LastPrice - customer.Price) / customer.Price : 0;
                        x.LastPrice = customer.LastPrice;
                        x.Name = customer.Name;
                        x.Price = customer.Price;
                        x.PriceHistory = customer.PriceHistory;
                        x.Shares =customer.Shares;
                        x.Symbol=customer.Symbol;
                        x.Value = customer.Value;
                           
                    });
                }
                catch (Exception e)
                {
                    error = e.Message;
                    success = false;
                }
                return new CollectionViewItemResult<PortfolioStatic.PortfolioFGridClass>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = customer
                };
            }, () => PortfolioStatic.PortfolioListFGrid.ToList<PortfolioStatic.PortfolioFGridClass>()));
        }
    
    
    }
}
