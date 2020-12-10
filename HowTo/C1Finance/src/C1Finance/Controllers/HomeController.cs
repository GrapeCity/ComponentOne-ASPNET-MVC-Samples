using C1.Web.Mvc;
using System;
using System.Linq;
using C1Finance.Models;
using C1.Web.Mvc.Serializition;
using Microsoft.AspNetCore.Mvc;

namespace C1MVCFinance.Controllers
{
    public class HomeController : Controller
    {
        //Controller for Index.cshtml
        public ActionResult Index()
        {
            return View();
        }

       //Get Portfolio
        public ActionResult GetPortfolio([C1JsonRequest]CollectionViewRequest<PortfolioStatic.Portfolio> requestData)
        {
            //To check if new Symbol has been requested, add the new symbol and return portfolio else return original portfolio
            const string stockNamesKey = "stockNames";
            if (requestData.ExtraRequestData != null && requestData.ExtraRequestData.ContainsKey(stockNamesKey))
            {
                var stockNameString = requestData.ExtraRequestData[stockNamesKey] as string;
                if (!string.IsNullOrEmpty(stockNameString))
                {
                    var stockNames = stockNameString.Split(',').ToList();
                    string SymbolToAdd = stockNames[stockNames.Count - 1];
                    PortfolioStatic.AddToPortfolioList(SymbolToAdd);
                }
            }
            
            return this.C1Json(CollectionViewHelper.Read(requestData, PortfolioModel.PortfolioList));
        }
        //Get Symbols
        public ActionResult GetStockNames([C1JsonRequest]CollectionViewRequest<PortfolioStatic.StockSymbol> requestData)
        {
           return this.C1Json(CollectionViewHelper.Read(requestData, PortfolioModel.SymbolList));
        }
        //To Delete selected portfolio
        public ActionResult GridDelete([C1JsonRequest]CollectionViewEditRequest<PortfolioStatic.Portfolio> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<PortfolioStatic.Portfolio>(requestData, customer =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    var result = PortfolioStatic.PortfolioList.FirstOrDefault(item => item.Symbol == customer.Symbol);
                    if (result != null)
                    {
                        PortfolioStatic.PortfolioList.Remove(result);
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
                return new CollectionViewItemResult<PortfolioStatic.Portfolio>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = customer
                };
            }, () => PortfolioStatic.PortfolioList.ToList<PortfolioStatic.Portfolio>()));
        }

        //To Update selected portfolio
        public ActionResult GridUpdate([C1JsonRequest]CollectionViewEditRequest<PortfolioStatic.Portfolio> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<PortfolioStatic.Portfolio>(requestData, customer =>
            {
                string error = string.Empty;
                bool success = true;
                try
                {
                    PortfolioStatic.PortfolioList.Where(x => x.Symbol == customer.Symbol).ToList().ForEach(x =>
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
                return new CollectionViewItemResult<PortfolioStatic.Portfolio>
                {
                    Error = error,
                    Success = success && ModelState.IsValid,
                    Data = customer
                };
            }, () => PortfolioStatic.PortfolioList.ToList<PortfolioStatic.Portfolio>()));
        }
    
    
    }
}
